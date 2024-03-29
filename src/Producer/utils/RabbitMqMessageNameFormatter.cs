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

namespace MTTests.Producer.utils
{
	public class RabbitMqMessageNameFormatter :
		IMessageNameFormatter
	{
		readonly IMessageNameFormatter _formatter;

		public RabbitMqMessageNameFormatter()
		{
			_formatter = new DefaultMessageNameFormatter("::", "--", ":", "-");
		}

		public string GetMessageName(Type type)
		{
			return _formatter.GetMessageName(type);
		}
	}
}