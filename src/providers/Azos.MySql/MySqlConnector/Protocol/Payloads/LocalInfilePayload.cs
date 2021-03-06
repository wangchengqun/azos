/*<FILE_LICENSE>
 * Azos (A to Z Application Operating System) Framework
 * The A to Z Foundation (a.k.a. Azist) licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
</FILE_LICENSE>*/
using System.Text;
using MySqlConnector.Protocol.Serialization;
using MySqlConnector.Utilities;

namespace MySqlConnector.Protocol.Payloads
{
	// See https://dev.mysql.com/doc/internals/en/com-query-response.html#local-infile-request
	internal sealed class LocalInfilePayload
	{
		public const byte Signature = 0xFB;

		public string FileName { get; }

		public static LocalInfilePayload Create(PayloadData payload)
		{
			var reader = new ByteArrayReader(payload.ArraySegment);
			reader.ReadByte(Signature);
			var fileName = Encoding.UTF8.GetString(reader.ReadByteArraySegment(reader.BytesRemaining));
			return new LocalInfilePayload(fileName);
		}

		private LocalInfilePayload(string fileName)
		{
			FileName = fileName;
		}
	}
}
