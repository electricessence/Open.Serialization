﻿using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Serialization
{
	/// <summary>
	/// Interface for deserializing any given generic type.
	/// </summary>
	public interface IDeserialize
	{
		/// <summary>
		/// Deserializes a string value to the specified type.
		/// </summary>
		/// <param name="value">The string to deserialize.</param>
		/// <returns>The deserialized result.</returns>
		T Deserialize<T>(string? value);

#if NETSTANDARD2_1
		ValueTask<T> DeserializeAsync<T>(Stream source, CancellationToken cancellationToken = default)
			=> DefaultMethods.DeserializeAsync<T>(this, source);
#endif
	}

	/// <summary>
	/// Interface for deserializing a predefined specific generic type.
	/// </summary>
	public interface IDeserialize<out T>
	{
		/// <summary>
		/// Deserializes a string value to the specified type.
		/// </summary>
		/// <param name="value">The string to deserialize.</param>
		/// <returns>The deserialized result.</returns>
		T Deserialize(string? value);
	}
}
