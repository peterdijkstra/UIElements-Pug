using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Debug = UnityEngine.Debug;

namespace Eidetic.Editor.Slim
{
	class PugProcessor : AssetPostprocessor
	{
		private const string Pug      = "/usr/local/bin/pug";
		private const string PathName = "PATH";
		private const string PathPath = "/usr/local/bin";

		static async void OnPostprocessAllAssets(
			string[] importedAssets,
			string[] deletedAssets,
			string[] movedAssets,
			string[] movedFromAssetPaths)
		{
			var pugFilePaths = importedAssets
				.Where(filePath => filePath.Split('.')[1] == "pug");

			foreach (var filePath in pugFilePaths)
			{
				var fileName = filePath.Split('.')[0];
				var dataPath = Application.dataPath.Replace("/Assets", "");
				var args     = $"{dataPath}/{fileName}.pug -P";

				var process = ProcessAsyncHelper.RunProcessAsync(
					Pug,
					args,
					1000,
					(PathName, PathPath));
				await process;

				// When it's done, insert the UIElements boilerplate

				var tempOutput  = File.ReadAllText(fileName + ".html");
				var insertIndex = tempOutput.IndexOf("<UXML", StringComparison.Ordinal);
				var uxmlString = tempOutput.Insert(insertIndex + 5,
					" xmlns:xsi=\"http:/www.w3.org/2001/XMLSchema-instance\""
					+ " xmlns=\"UnityEngine.UIElements\""
					+ " xsi:noNamespaceSchemaLocation=\"../UIElementsSchema/UIElements.xsd\""
					+ " xsi:schemaLocation=\"UnityEngine.UIElements ../UIElementsSchema/UnityEngine.UIElements.xsd\"");

				File.Delete(fileName + ".html");

				var uxmlDoc = new XmlDocument();
				uxmlDoc.LoadXml(uxmlString);

				var settings = new XmlWriterSettings {Indent = true};
				// Save the document to a file and auto-indent the output.
				var writer = XmlWriter.Create(fileName + ".uxml", settings);
				uxmlDoc.Save(writer);
				writer.Dispose();
			}
		}
	}
}