
using System;
using System.Collections.Generic;
using System.IO;
using MorseChat.MorseChat.Internal;
using MorseChat.MorseChat.Model;
using MorseChat.MorseChat.Service;
using MorseChat.MorseChat.Util;
using Xunit;
using Xunit.Abstractions;

namespace MorseChatTest.MorseChat.Service
{
	public class EncodeServiceTest
	{
		private readonly ITestOutputHelper _testOutputHelper;
		private readonly IEncodeService<MorseText> _encodeService;

		public EncodeServiceTest(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;
			_encodeService = new MorseEncodeService(new MorseToEngTranslator());
		}

		[Fact]
		public void EncodingWorksForExactValue()
		{
			var result = _encodeService.Encode("Hello");
			var expectedResult =
				StringToMorseTextTranlator.Translate(".... . .-.. .-.. ---");
			Assert.Equal(result,expectedResult);
		}

		[Fact]
		public void EncodeServiceThrowsExceptionWhenSymbolIsInvalid()
		{
			var exception = Assert.Throws<FormatException>(() => _encodeService.Encode("#(!(#)"));
			Assert.Equal("Invalid symbol",exception.Message);

		}
		[Fact]
		public void EncodeServiceThrowsExceptionWhenMessageIsEmpty()
		{
			var exception = Assert.Throws<InvalidDataException>(() => _encodeService.Encode(""));
			Assert.Equal("Message is empty",exception.Message);

		}
	}
}

