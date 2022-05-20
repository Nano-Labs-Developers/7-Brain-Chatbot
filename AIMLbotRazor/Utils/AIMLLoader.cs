﻿using System;
using System.IO;
using System.Text;
using System.Xml;
using AIMLbotRazor;
using System.Reflection;
using AIMLbotRazor.Normalize;
using System.Linq;

namespace AIMLbotRazor.Utils
{
	public class AIMLLoader
    {
		private Bot bot;

		public AIMLLoader(Bot bot)
		{
			this.bot = bot;
		}

		public void LoadAIML()
		{
			string[] files = GetAllAIML();

			if (files.Length > 0)
			{
				foreach (string filename in files)
				{
					LoadAIMLFile(filename);
				}
				return;
			}
			throw new FileNotFoundException("Could not find any .aiml files in the specified embedded resources.");
		}
		
		private string[] GetAllAIML()
		{
			var executingAssembly = Assembly.GetExecutingAssembly();
			string folderName = string.Format("{0}.aiml.", executingAssembly.GetName().Name);
			return executingAssembly
				.GetManifestResourceNames()
				.Where(r => r.StartsWith(folderName) && r.EndsWith(".aiml"))
				.ToArray();
		}
		
		private string GetAllAIML(string aimlfile)
		{
			var executingAssembly = Assembly.GetExecutingAssembly();
			
			using (Stream stream = executingAssembly.GetManifestResourceStream(aimlfile))
			{
				using (StreamReader sr = new StreamReader(stream))
				{
					return sr.ReadToEnd();
				}
			}
		}

		public void LoadAIMLFile(string filename)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(GetAllAIML(filename));
			Console.WriteLine("AIML File: " + filename);
			LoadAIMLFromXML(xmlDocument, filename);
		}

		public void LoadAIMLFromXML(XmlDocument doc, string filename)
		{
			Console.WriteLine(doc+filename);
			XmlNodeList childNodes = doc.DocumentElement.ChildNodes;
			foreach (XmlNode item in childNodes)
			{
				if (item.Name == "topic")
				{
					processTopic(item, filename);
				}
				else if (item.Name == "category")
				{
					processCategory(item, filename);
				}
			}
		}

		private void processTopic(XmlNode node, string filename)
		{
			string topicName = "*";
			if ((node.Attributes.Count == 1) & (node.Attributes[0].Name == "name"))
			{
				topicName = node.Attributes["name"].Value;
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.Name == "category")
				{
					processCategory(childNode, topicName, filename);
				}
			}
		}

		private void processCategory(XmlNode node, string filename)
		{
			processCategory(node, "*", filename);
		}

		private void processCategory(XmlNode node, string topicName, string filename)
		{
			XmlNode xmlNode = FindNode("pattern", node);
			XmlNode xmlNode2 = FindNode("template", node);
			if (object.Equals(null, xmlNode))
			{
				throw new XmlException("Missing pattern tag in a node found in " + filename);
			}
			if (object.Equals(null, xmlNode2))
			{
				throw new XmlException("Missing template tag in the node with pattern: " + xmlNode.InnerText + " found in " + filename);
			}
			string text = generatePath(node, topicName, isUserInput: false);
			if (text.Length > 0)
			{
				try
				{
					bot.Graphmaster.addCategory(text, xmlNode2.OuterXml, filename);
					bot.Size++;
					return;
				}
				catch
				{
					return;
				}
			}
			}

		public string generatePath(XmlNode node, string topicName, bool isUserInput)
		{
			XmlNode xmlNode = FindNode("pattern", node);
			XmlNode xmlNode2 = FindNode("that", node);
			string that = "*";
			string pattern = ((!object.Equals(null, xmlNode)) ? xmlNode.InnerText : string.Empty);
			if (!object.Equals(null, xmlNode2))
			{
				that = xmlNode2.InnerText;
			}
			return generatePath(pattern, that, topicName, isUserInput);
		}

		private XmlNode FindNode(string name, XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.Name == name)
				{
					return childNode;
				}
			}
			return null;
		}

		public string generatePath(string pattern, string that, string topicName, bool isUserInput)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string empty = string.Empty;
			string text = "*";
			string text2 = "*";
			if (bot.TrustAIML && !isUserInput)
			{
				empty = pattern.Trim();
				text = that.Trim();
				text2 = topicName.Trim();
			}
			else
			{
				empty = Normalize(pattern, isUserInput).Trim();
				text = Normalize(that, isUserInput).Trim();
				text2 = Normalize(topicName, isUserInput).Trim();
			}
			if (empty.Length > 0)
			{
				if (text.Length == 0)
				{
					text = "*";
				}
				if (text2.Length == 0)
				{
					text2 = "*";
				}
				if (text.Length > bot.MaxThatSize)
				{
					text = "*";
				}
				stringBuilder.Append(empty);
				stringBuilder.Append(" <that> ");
				stringBuilder.Append(text);
				stringBuilder.Append(" <topic> ");
				stringBuilder.Append(text2);
				return stringBuilder.ToString();
			}
			return string.Empty;
		}

		public string Normalize(string input, bool isUserInput)
		{
			StringBuilder stringBuilder = new StringBuilder();
			ApplySubstitutions applySubstitutions = new ApplySubstitutions(bot);
			StripIllegalCharacters stripIllegalCharacters = new StripIllegalCharacters(bot);
			string text = applySubstitutions.Transform(input);
			string[] array = text.Split(" \r\n\t".ToCharArray());
			string[] array2 = array;
			foreach (string text2 in array2)
			{
				string text3 = ((!isUserInput) ? ((!(text2 == "*") && !(text2 == "_")) ? stripIllegalCharacters.Transform(text2) : text2) : stripIllegalCharacters.Transform(text2));
				stringBuilder.Append(text3.Trim() + " ");
			}
			return stringBuilder.ToString().Replace("  ", " ");
		}
	}
}
