﻿// Copyright 2012 Christian Jacobsen
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.

using System;
using System.Collections.Generic;
using System.Text;

namespace MTTests.Producer.utils
{
	public class DefaultMessageNameFormatter :
		IMessageNameFormatter
	{
		readonly Dictionary<Type, string> _cache;
		readonly string _genericArgumentSeparator;
		readonly string _genericTypeSeparator;
		readonly string _namespaceSeparator;
		readonly string _nestedTypeSeparator;

		public DefaultMessageNameFormatter(string genericArgumentSeparator, string genericTypeSeparator,
										   string namespaceSeparator, string nestedTypeSeparator)
		{
			_genericArgumentSeparator = genericArgumentSeparator;
			_genericTypeSeparator = genericTypeSeparator;
			_namespaceSeparator = namespaceSeparator;
			_nestedTypeSeparator = nestedTypeSeparator;

			_cache = new Dictionary<Type, string>();
		}

		public string GetMessageName(Type type)
		{
			if (!_cache.ContainsKey(type)) return _cache[type] = CreateMessageName(type);
			return _cache[type];
		}

		string CreateMessageName(Type type)
		{
			if (type.IsGenericTypeDefinition)
				throw new ArgumentException("An open generic type cannot be used as a message name");

			var sb = new StringBuilder("");

			return GetMessageName(sb, type, null);
		}

		string GetMessageName(StringBuilder sb, Type type, string scope)
		{
			if (type.IsGenericParameter)
				return "";

			if (type.Namespace != null)
			{
				string ns = type.Namespace;
				if (!ns.Equals(scope))
				{
					sb.Append(ns);
					sb.Append(_namespaceSeparator);
				}
			}

			if (type.IsNestedPublic)
			{
				GetMessageName(sb, type.DeclaringType, type.Namespace);
				sb.Append(_nestedTypeSeparator);
			}

			if (type.IsGenericType)
			{
				string name = type.GetGenericTypeDefinition().Name;

				//remove `1
				int index = name.IndexOf('`');
				if (index > 0)
					name = name.Remove(index);

				sb.Append(name);
				sb.Append(_genericTypeSeparator);

				Type[] arguments = type.GetGenericArguments();
				for (int i = 0; i < arguments.Length; i++)
				{
					if (i > 0)
					{
						sb.Append(_genericArgumentSeparator);
					}

					GetMessageName(sb, arguments[i], type.Namespace);
				}

				sb.Append(_genericTypeSeparator);
			}
			else
				sb.Append(type.Name);

			return sb.ToString();
		}
	}
}