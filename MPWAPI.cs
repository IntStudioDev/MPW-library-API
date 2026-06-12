using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;

using UnityEngine;

namespace MPW;

/// <summary> API for working with the MPW library. </summary>
public class MPWAPI
{
	/// <summary> Is api initialized? </summary>
	public virtual bool IsInitialized
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => Status == APIStatus.Success;
	}
	/// <summary> Current API status. </summary>
	public virtual APIStatus Status => _Status;
	/// <summary> Invoked when API status changed. 
	/// <para>
	/// When subscribed, it immediately sends the current status, unless it is <see cref="APIStatus.None"/>. 
	/// </para>
	/// </summary>
	public event ApiStatusChangedHandler? StatusChanged
	{
		add => StatusChanged_Subscribe(value);
		remove => StatusChanged_Unsubscribe(value);
	}

	/// <summary> MPW services. </summary>
	public virtual IServiceProvider Services
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			switch (Status)
			{
				case APIStatus.None:
				{
					throw new InvalidOperationException("The API has not started initialization.");
				}
				case APIStatus.Processing:
				{
					throw new InvalidOperationException("The API sends a request or waits for a response from the library.");
				}
				case APIStatus.MPWNotFound:
				{
					throw new LibraryNotFoundException("The API could not find MPW library service provider type (most likely, the library hasn't been loaded).");
				}
				case APIStatus.ResponseError:
				{
					throw new AggregateException("The MPW library's response contains an errors. Check previous logs.");
				}
				case APIStatus.Success:
				{
					return _ServiceProvider!;
				}
				default:
				{
					throw new ArgumentOutOfRangeException(nameof(Status), _Status, "Unknown API status.");
				}
			}
		}
	}

	/// <summary> Mod that uses the API. </summary>
	public virtual ModMetaData Mod { get; }


	/// <summary> Create api for mod. </summary>
	/// <param name="mod"> Mod that will use the API.</param>
	/// <exception cref="ArgumentNullException"> Thrown if <paramref name="mod"/> is <see langword="null"/>. </exception>
	/// <exception cref="TypeLoadException"> Thrown if failed to get MPW service provider type. </exception>
	/// <exception cref="MissingMethodException"> Thrown if failed to get request method. </exception>
	public static MPWAPI Create(ModMetaData mod)
	{
		if (mod == null)
		{
			throw new ArgumentNullException(nameof(mod));
		}

		MPWAPI api = new MPWAPI(mod);
		api.StartInitializing();

		return api;
	}


	/// <inheritdoc/>
	public override string ToString() => $"\"{Mod.Name}\"`s MPWAPI";

	#region NonPublic
	/// <inheritdoc cref="Services"/>
	protected IServiceProvider? _ServiceProvider;
	/// <inheritdoc cref="Status"/>
	protected APIStatus _Status;

	/// <summary> Backing event for <see cref="StatusChanged"/>, </summary>
	protected event ApiStatusChangedHandler? _StatusChanged;

	/// <inheritdoc/>
	protected MPWAPI(ModMetaData mod)
	{
		if (mod == null)
		{
			throw new ArgumentNullException(nameof(mod));
		}
		Mod = mod;
	}

	/// <summary> Start initializing API. </summary>
	/// <inheritdoc cref="SendRequest(ProviderRequest)"/>
	/// <inheritdoc cref="OnResponse_StatusChanged(ProviderResponse)"/>
	protected virtual void StartInitializing()
	{
		SetStatus(APIStatus.Processing, false);
		ProviderResponse response;
		try
		{
			response = SendRequest(new ProviderRequest(Mod, DateTime.UtcNow));
		}
		catch (TypeLoadException) // type not found
		{
			RetryInitialize();
			return;
		}

		if (response.IsCompleted)
		{
			OnResponse_StatusChanged(response);
		}
		else
		{
			response.StatusChanged += OnResponse_StatusChanged;
		}
	}

	/// <summary>
	/// It is attempting to reinitialize.
	/// </summary>
	/// <remarks> By default, this starts a coroutine that makes several attempts to send the request. </remarks>
	protected virtual void RetryInitialize()
	{
		BackgroundItemLoader.Instance!.StartCoroutine(InitializeRoutine(TimeSpan.FromSeconds(1), 5));
	}
	/// <summary>
	/// Attempts to send the request <paramref name="attempts"/> times, with a <paramref name="requestSendDelay"/> between each attempt.
	/// </summary>
	/// <param name="requestSendDelay"> Delay between requests. </param>
	/// <param name="attempts"> Max attempts. </param>
	protected virtual IEnumerator InitializeRoutine(TimeSpan requestSendDelay, int attempts)
	{
		if (attempts <= 0)
		{
			yield break;
		}

		WaitForSecondsRealtime wait = new WaitForSecondsRealtime((float)requestSendDelay.TotalSeconds);

		List<Exception> exceptions = new List<Exception>(attempts);
		int currentAttempts = attempts;
		while (currentAttempts > 0)
		{
			currentAttempts--;
			yield return wait;

			ProviderResponse response;
			try
			{
				response = SendRequest(new ProviderRequest(Mod, DateTime.UtcNow));
			}
			catch (Exception ex)
			{
				exceptions.Add(ex);
				continue;
			}


			exceptions.Clear();
			if (response.IsCompleted)
			{
				OnResponse_StatusChanged(response);
			}
			else
			{
				response.StatusChanged += OnResponse_StatusChanged;
			}
			yield break;
		}
		SetStatus(APIStatus.MPWNotFound);

		StringBuilder message = new StringBuilder(128 + (64 * attempts));
		message.Append($"{this}: failed to get response from MPW (attempts: {attempts}), exceptions: ");

		for (int i = 0; i < exceptions.Count; i++)
		{
			message.AppendLine();
			message.Append(" [");
			message.Append(i);
			message.Append("] ");

			message.Append(exceptions[i]);
		}
		exceptions.Clear();

		Debug.LogError(message.ToString());
		// show "Not found" window ?
	}

	/// <summary> Send service provider request. </summary>
	/// <returns> MPW provider response. </returns>
	/// <exception cref="ArgumentNullException"> Thrown if <paramref name="request"/> is null. </exception>
	/// <exception cref="TypeLoadException"> Thrown if failed to get MPW service provider type. </exception>
	/// <exception cref="MissingMethodException"> Thrown if failed to get request method. </exception>
	/// <exception cref="InvalidResponseTypeException"> Thrown if MPW response has invalid type. </exception>
	protected virtual ProviderResponse SendRequest(ProviderRequest request)
	{
		if (request == null)
		{
			throw new ArgumentNullException(nameof(request));
		}

		Type providerType = Type.GetType("MPW.ServiceProvider, MPW Library, Version=null, Culture=neutral, PublicKeyToken=null", true); // assembly name
		Debug.Log("provider type:" + providerType.AssemblyQualifiedName);

		const string requestMethodName = "RequestProvider";
		MethodInfo? requestMethod = providerType.GetMethod(requestMethodName, BindingFlags.Public | BindingFlags.Static, null, [typeof(ProviderRequest)], null);
		if (requestMethod == null)
		{
			Debug.Log("Failed to get request method.");
			throw new MissingMethodException($"Failed to get {providerType.FullName}.{requestMethodName} method. " +
				$"The problem is most likely caused by multiple MPW.API assemblies being loaded. Remove the MPW.API.dll load from your mod.");
		}

		object? result = requestMethod.Invoke(null, [request]);
		Debug.Log($"request result: {result} ({result?.GetType()})");
		if (result is ProviderResponse response)
		{
			return response;
		}
		throw new InvalidResponseTypeException(result == null ? null : result.GetType(), typeof(ProviderResponse));
	}

	/// <summary> Invoked when response. </summary>
	/// <exception cref="ArgumentNullException"></exception>
	protected virtual void OnResponse_StatusChanged(ProviderResponse response)
	{
		if (response == null)
		{
			throw new ArgumentNullException(nameof(response));
		}
		switch (response.Status)
		{
			case ResponseStatus.Error:
			{
				Debug.LogError($"{this}: Failed to get service provider. Errors: {string.Join(",\n", response.Errors)}");
				response.StatusChanged -= OnResponse_StatusChanged;
				SetStatus(APIStatus.ResponseError);
				break;
			}
			case ResponseStatus.Success:
			{
				_ServiceProvider = response.ServiceProvider;
				response.StatusChanged -= OnResponse_StatusChanged;
				Debug.Log($"{this}: MPW service provider has been successfully obtained.");

				SetStatus(APIStatus.Success);
				break;
			}
			case ResponseStatus.None:
			case ResponseStatus.Processing:
			default:
			{
				break;
			}
		}
	}

	/// <summary> Invoke <see cref="StatusChanged"/> event. </summary>
	protected virtual void SetStatus(APIStatus status, bool notify = true)
	{
		_Status = status;
		if (!notify || _StatusChanged == null)
		{
			return;
		}

		try
		{
			_StatusChanged.Invoke(this, status);
		}
		catch (Exception ex)
		{
			Debug.LogError($"{this}: Failed to invoke {nameof(StatusChanged)} event: {ex}");
		}
		if (status == APIStatus.Success)
		{
			_StatusChanged = null;
		}
	}

	/// <summary> Subscribe to <see cref="StatusChanged"/> event. </summary>
	protected virtual void StatusChanged_Subscribe(ApiStatusChangedHandler? handler)
	{
		if (handler == null)
		{
			return;
		}
		if (_Status != APIStatus.None)
		{
			try
			{
				handler.Invoke(this, _Status);
			}
			catch (Exception ex)
			{
				Debug.LogError($"{this}: Failed to invoke {nameof(StatusChanged)} event handler ({handler}) on subscribe: {ex}");
				return;
			}
		}

		if (_Status != APIStatus.Success)
		{
			_StatusChanged += handler;
		}
	}
	/// <summary> Unsubscribe from <see cref="StatusChanged"/> event. </summary>
	protected virtual void StatusChanged_Unsubscribe(ApiStatusChangedHandler? handler) => _StatusChanged -= handler;
	#endregion

	#region Types
	/// <summary> MPW API status. </summary>
	public enum APIStatus
	{
		/// <summary> The API has not started initialization. </summary>
		None,
		/// <summary> The API sends a request or waits for a response from the library. </summary>
		Processing,
		/// <summary> The API could not find MPW library service provider type (most likely, the library hasn't been loaded). </summary>
		MPWNotFound,
		/// <summary> The MPW library's response contains an error. </summary>
		ResponseError,
		/// <summary> The service provider was successfully received. </summary>
		Success,
	}

	/// <summary> MPW service provider request. </summary>
	public class ProviderRequest : IEquatable<ProviderRequest>
	{
		/// <summary> Mod that requests a service provider. </summary>
		public readonly ModMetaData Mod;
		/// <summary> The time the request was sent (UTC). </summary>
		public readonly DateTime TimeStamp;


		/// <inheritdoc/>
		public ProviderRequest(ModMetaData mod, DateTime timeStamp)
		{
			if (mod == null)
			{
				throw new ArgumentNullException(nameof(mod));
			}

			Mod = mod;
			TimeStamp = timeStamp;
		}

		/// <inheritdoc/>
		public bool Equals(ProviderRequest other) => Mod == other.Mod && TimeStamp == other.TimeStamp;
		/// <inheritdoc/>
		public override bool Equals(object obj) => obj is ProviderRequest other && Equals(other);
		/// <inheritdoc/>
		public override int GetHashCode() => HashCode.Combine(Mod, TimeStamp);

	}

	/// <summary> MPW service provider response. </summary>
	public class ProviderResponse
	{
		/// <summary> MPW service provider. </summary>
		public IServiceProvider? ServiceProvider { get; set; }

		/// <summary> Current response status. </summary>
		public virtual ResponseStatus Status
		{
			get => _Status;
			set
			{
				if (_Status != value)
				{
					_Status = value;
					InvokeStatusChanged(_Status);
				}
			}
		}
		/// <summary> Invoked when response status changed. </summary>
		public event ResponseStatusChangedHandler? StatusChanged;

		/// <inheritdoc cref="ResponseStatus.Success"/>
		public bool IsSuccess => Status == ResponseStatus.Success;
		/// <summary> The request has been processed (with or without an error). </summary>
		public bool IsCompleted => Status != ResponseStatus.None && Status != ResponseStatus.Processing;

		/// <summary> Errors. </summary>
		public virtual IReadOnlyList<Exception> Errors { get; set; }

		private ResponseStatus _Status = ResponseStatus.None;

		/// <summary> Crete empty <see cref="ProviderResponse"/>. </summary>
		public ProviderResponse()
		{
			Errors = Array.Empty<Exception>();
		}
		/// <summary> Create success <see cref="ProviderResponse"/> with specified <see cref="IServiceProvider"/>. </summary>
		public ProviderResponse(IServiceProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException(nameof(provider));
			}
			ServiceProvider = provider;
			Status = ResponseStatus.Success;
			Errors = Array.Empty<Exception>();
		}

		/// <summary> Invoke <see cref="StatusChanged"/> event. </summary>
		/// <exception cref="Exception"></exception>
		protected virtual void InvokeStatusChanged(ResponseStatus status)
		{
			try
			{
				StatusChanged?.Invoke(this);
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to invoke {nameof(StatusChanged)} event", ex);
			}
		}
	}
	/// <summary> Response status. </summary>
	public enum ResponseStatus : byte
	{
		/// <summary> The request has not started processing. </summary>
		None,
		/// <summary> Request is currently being processed. </summary>
		Processing,
		/// <summary> An error occurred during processing. </summary>
		Error,
		/// <summary> Request has been successfully processed. </summary>
		Success,
	}

	/// <summary> Response status changed event handler. </summary>
	public delegate void ResponseStatusChangedHandler(ProviderResponse response);

	/// <summary> API status change event handler. </summary>
	/// <param name="api"> API instance. </param>
	/// <param name="status"> Current API status. </param>
	public delegate void ApiStatusChangedHandler(MPWAPI api, APIStatus status);


	/// <summary> Indicates that the library search was unsuccessful. </summary>
	public class LibraryNotFoundException : Exception
	{
		/// <inheritdoc/>
		public LibraryNotFoundException() { }
		/// <inheritdoc/>
		public LibraryNotFoundException(string message) : base(message) { }

		/// <inheritdoc/>
		public LibraryNotFoundException(string message, Exception innerException) : base(message, innerException) { }
		/// <inheritdoc/>
		protected LibraryNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
	/// <summary> Indicates an error related to an incorrect response type. </summary>
	public class InvalidResponseTypeException : Exception
	{
		/// <inheritdoc/>
		public InvalidResponseTypeException(Type? actualType, Type requiredType) :
			base($"Invalid response type, actualType: {(actualType == null ? "null" : actualType)}, must be {requiredType}")
		{ }
		/// <inheritdoc/>
		public InvalidResponseTypeException(string message) : base(message) { }
	}
	#endregion
}
