using System;
using System.Collections.Generic;
using System.IO;
using HtmlAgilityPack;

namespace NyaWatch.Core.Parsing
{
	public interface IParser
	{
		Dictionary<string, string> ParseAnimeFromString(string html);
		Dictionary<string, string> ParseAnimeFromFile(string path);
		Dictionary<string, string> ParseAnimeFromWeb(string url);

		Dictionary<string, string> ParseAnimePreviewFromString(string html);
		Dictionary<string, string> ParseAnimePreviewFromFile(string path);
		Dictionary<string, string> ParseAnimePreviewFromWeb(string url);
	}

	public abstract class Parser : IParser
	{
		public Dictionary<string, string> ParseAnimeFromString(string html)
		{
			var doc = new HtmlDocument ();
			doc.LoadHtml (html);
			return ParseAnime (doc);
		}

		public Dictionary<string, string> ParseAnimeFromFile(string path)
		{
			var doc = new HtmlDocument ();
			doc.Load (path);
			return ParseAnime (doc);
		}

		public Dictionary<string, string> ParseAnimeFromWeb(string url)
		{
			var web = new HtmlWeb ();
			web.AutoDetectEncoding = false;
			web.OverrideEncoding = System.Text.Encoding.GetEncoding ("windows-1251");
			var doc = web.Load (url);
			return ParseAnime (doc);
			
			/*var document = new HtmlDocument();
			using (var client = new System.Net.WebClient())
			{
				using (var stream = client.OpenRead(url))
				{
					var reader = new StreamReader(stream, System.Text.Encoding.GetEncoding("windows-1251"));
					var html = reader.ReadToEnd();
					document.LoadHtml(html);
				}
			}
			return ParseAnime(document);*/
		}

		public Dictionary<string, string> ParseAnimePreviewFromString(string html)
		{
			var doc = new HtmlDocument ();
			doc.LoadHtml (html);
			return ParseAnimePreview (doc);
		}

		public Dictionary<string, string> ParseAnimePreviewFromFile(string path)
		{
			var doc = new HtmlDocument ();
			doc.Load (path);
			return ParseAnimePreview (doc);
		}

		public Dictionary<string, string> ParseAnimePreviewFromWeb(string url)
		{
			/*var web = new HtmlWeb ();
			web.AutoDetectEncoding = false;
			web.OverrideEncoding = System.Text.Encoding.GetEncoding ("windows-1251");
			var doc = web.Load (url);
			return ParseAnimePreview (doc);*/


			var document = new HtmlDocument();
			using (var client = new System.Net.WebClient())
			{
				using (var stream = client.OpenRead(url))
				{
					var reader = new StreamReader(stream, System.Text.Encoding.GetEncoding("windows-1251"));
					var html = reader.ReadToEnd();
					document.LoadHtml(html);
				}
			}
			return ParseAnimePreview(document);
		}

		protected abstract Dictionary<string, string> ParseAnime(HtmlDocument doc);
		protected abstract Dictionary<string, string> ParseAnimePreview(HtmlDocument doc);
	}
}

