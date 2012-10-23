using System;

namespace MTTests.Producer.utils
{
	/// <summary>
	/// Used to format a message type into a MessageName, which can be used as a valid
	/// queue name on the transport
	/// </summary>
	public interface IMessageNameFormatter
	{
		string GetMessageName(Type type);
	}
}