﻿using System.Threading;
using Utf8Json;

namespace Open.Serialization.Json.System
{
	public class JsonSerializerFactory : JsonSerializerFactoryBase
	{
		static readonly IJsonFormatterResolver DefaultOptions = RelaxedJson.Options();
		readonly IJsonFormatterResolver _options;
		public JsonSerializerFactory(IJsonFormatterResolver defaultOptions = null)
		{
			_options = defaultOptions?.Clone() ?? DefaultOptions;
		}

		JsonSerializerInternal _default;
		JsonSerializerInternal _caseSensitive;
		JsonSerializerInternal _ignoreCase;

		protected override SerializerBase GetDeserializerInternal(bool caseSensitive)
			=> caseSensitive
				? LazyInitializer.EnsureInitialized(ref _caseSensitive,
					() => new JsonSerializerInternal(_options.Clone().SetPropertyNameCaseInsensitive(false)))
				: LazyInitializer.EnsureInitialized(ref _ignoreCase,
					() => new JsonSerializerInternal(_options.Clone().SetPropertyNameCaseInsensitive(true)));
		
		protected override SerializerBase GetSerializerInternal(IJsonSerializationOptions options)
		{
			if (options == null)
				return LazyInitializer.EnsureInitialized(ref _default, () => new JsonSerializerInternal(_options));

			var o = _options.Clone();
			o.IgnoreNullValues = options.OmitNull;
			o.WriteIndented = options.Indent;
			o.DictionaryKeyPolicy = options.CamelCaseKeys ? JsonNamingPolicy.CamelCase : null;
			o.PropertyNamingPolicy = options.CamelCaseProperties ? JsonNamingPolicy.CamelCase : null;

			return new JsonSerializerInternal(o);
		}
	}
}