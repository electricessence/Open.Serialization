﻿using System.Threading.Tasks;

namespace Open.Serialization
{
	public interface IDeserializeAsync
	{
		ValueTask<T> DeserializeAsync<T>(string value);
	}

	public interface IDeserializeAsync<T>
	{
		ValueTask<T> DeserializeAsync(string value);
	}
}
