using System;
using MorseChat.MorseChat.Internal;
using MorseChat.MorseChat.Model;
using MorseChat.MorseChat.Service;
using MorseChat.MorseChat.Util;
using Xunit;

namespace MorseChatTest.MorseChat.Service
{
	public class DecodeServiceTest
	{
		private readonly IDecodeService<MorseText> _decodeService;

		public DecodeServiceTest()
		{
			_decodeService = new MorseDecodeService(new MorseToEngTranslator());
		}

		[Fact]
		public void DecodeWorksForExactValue()
		{
			var text = ".- -... -.-. -..";
			var message = StringToMorseTextTranlator.Translate(text);
			var result = _decodeService.Decode(message);
			Assert.Equal("ABCD", result);
		}

		[Fact]
		public void DecodeThorwsExceptionWhenSymbolIsInvalid()
		{
			var text = ".......- -.-.";
			var message = StringToMorseTextTranlator.Translate(text);
			var exception = Assert.Throws<FormatException>(() => _decodeService.Decode(message));
			Assert.Equal("Symbol is undecodable",exception.Message);
		}
	}
}
