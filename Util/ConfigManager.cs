using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Bmse.Common;

/*
 * 以下のようにXMLファイルを管理する
 * 
 * ROOT要素の部分は、
 * サブフォルダがあればサブフォルダ名、
 * なければXMLファイルのベース名になる。
 * 
 * <?xml version="1.0" encoding="UTF-8"?>
 * <ROOT>
 *   <item name="NAME">
 *     <ELEMENT>VALUE</ELEMENT>
 *     ..
 *   </item>
 *   ..
 * </ROOT>
 * 
 */

namespace Bmse.Util
{
	struct XmlFile
	{
		public string root;		// XMLルート要素
		//public string path;		// XMLファイルのパス
		public XmlDocument xmlDocument;
		public XmlFile(string root/*, string path*/)
		{
			this.root = root;
			//this.path = path;
			xmlDocument = new XmlDocument();
		}
	}

	/*
	public enum XmlType
	{
		VIEWER,
		COMMON
	}
	 */

	public sealed class ConfigManager
	{
		// XMLのパスをキーに、データを格納する
		private Dictionary<string/*XmlType*/, XmlFile> document;

		/// <summary>
		/// XMLファイルの存在を調べます。
		/// </summary>
		/// <param name="path">XMLファイルのパス</param>
		/// <returns>ファイルが存在するか</returns>
		public bool Exist(string path/*XmlType type*/)
		{
			// documentに存在しないパスなら追加する
			AddNewDocument(path);

			//string path = document[type].path;
			XmlDocument xdoc = document[path/*type*/].xmlDocument;

			try
			{
				xdoc.Load(path);
			}
			catch (XmlException e)
			{
				// XML 内に読み込みエラーまたは解析エラーがあります。
				return false;
			}
			catch (FileNotFoundException e)
			{
				// 指定されたファイルが見つかりませんでした。
				return false;
			}
			catch (NotSupportedException e)
			{
				// ファイルの形式が無効です。
				return false;
			}
			catch (Exception e)
			{
				return false;
			}

			return true;
		}



		/// <summary>
		/// Item要素のリストを取得します。
		/// リストの中身はname含む要素と、対応する値のハッシュマップです。
		/// </summary>
		/// <param name="path">XMLファイルのパス</param>
		/// <returns>XMLファイルの中のItem要素のリスト</returns>
		public List<Dictionary<string, string>> ItemList(string path/*XmlType type*/)
		{
			// documentに存在しないパスなら追加する
			AddNewDocument(path);

			List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

			string root = document[path/*type*/].root;
			//string path = document[type].path;
			XmlDocument xdoc = document[path/*type*/].xmlDocument;

			string item_name = "";
			string item_element = "";
			string item_value = "";

			foreach (XmlElement item_elem in xdoc.DocumentElement)
			{
				if (!"".Equals(item_elem.GetAttribute("name")))
				{
					Dictionary<string, string> dic = new Dictionary<string, string>();

					item_name = item_elem.GetAttribute("name");
					dic.Add("name", item_name);

					XmlNodeList child_nodes = item_elem.ChildNodes;
					foreach (XmlNode child_node in child_nodes)
					{
						item_element = child_node.Name;
						item_value = child_node.InnerText;

						dic.Add(item_element, item_value);
					}

					list.Add(dic);
				}
			}

			return list;
		}



		/// <summary>
		/// XMLファイルから指定された要素の属性値を取得します。
		/// XMLファイルが無かったり、指定した要素が存在しないなどの場合は、defaultValueを返します。
		/// </summary>
		/// <param name="path">XMLファイルのパス</param>
		/// <param name="group">親要素の名前</param>
		/// <param name="item">子要素</param>
		/// <param name="defaultValue">規定値</param>
		/// <returns></returns>
		public string GetValue(string path/*XmlType type*/, string name, string element, string defaultValue)
		{
			// documentに存在しないパスなら追加する
			AddNewDocument(path);

			//// XMLファイルを読み込む
			////	→ファイルが見つからないなどの場合、defaultValueを返す
			//// 指定されたnameをname属性値に持つitem要素を探す
			//// 見つかったitem要素の中の、elementに一致する名前の子要素を探す
			//// その子要素のinnnerTextを返す
			////	→いずれかの条件に引っかからなかった場合はdefaultValueを返す

			string root = document[path/*type*/].root;
			//string path = document[type].path;
			XmlDocument xdoc = document[path/*type*/].xmlDocument;

			try
			{
				xdoc.Load(path);
			}
			catch
			{
				return defaultValue;
			}

			foreach (XmlElement item_elem in xdoc.DocumentElement)
			{
				if (name.Equals(item_elem.GetAttribute("name")))
				{
					XmlNodeList child_nodes = item_elem.ChildNodes;
					foreach (XmlNode child_node in child_nodes)
					{
						// <item>の子要素で、elementと一致する名前の要素を探す
						if (element.Equals(child_node.Name))
						{
							return child_node.InnerText;
						}
					}
				}
			}

			return defaultValue;
		}



		/// <summary>
		/// XMLファイルから指定された要素の属性値を取得します。
		/// XMLファイルが無かったり、指定した要素が存在しないなどの場合は、defaultValueを返します。
		/// </summary>
		/// <param name="path">XMLファイルのパス</param>
		/// <param name="group">親要素の名前</param>
		/// <param name="item">子要素</param>
		/// <param name="defaultValue">規定値</param>
		/// <returns></returns>
		public int GetValue(string path/*XmlType type*/, string name, string element, int defaultValue)
		{
			return int.Parse(GetValue(path, name, element, defaultValue.ToString()));
		}



		/// <summary>
		/// XMLファイルから指定された要素の属性値を取得します。
		/// XMLファイルが無かったり、指定した要素が存在しないなどの場合は、defaultValueを返します。
		/// </summary>
		/// <param name="path">XMLファイルのパス</param>
		/// <param name="group">親要素の名前</param>
		/// <param name="item">子要素</param>
		/// <param name="defaultValue">規定値</param>
		/// <returns></returns>
		public bool GetValue(string path/*XmlType type*/, string name, string element, bool defaultValue)
		{
			string defaultValueStr = defaultValue ? "true" : "false";

			if ("true".Equals(GetValue(path, name, element, defaultValueStr)))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		

		/// <summary>
		/// XMLファイルに指定された要素の属性値を書き込みます。
		/// </summary>
		/// <param name="path">XMLファイルのパス</param>
		/// <param name="name">親要素の名前</param>
		/// <param name="element">子要素</param>
		/// <param name="value">子要素の値</param>
		public void SetValue(string path/*XmlType type*/, string name, string element, string value)
		{
			// documentに存在しないパスなら追加する
			AddNewDocument(path);

			// まず既存のXMLファイルがあればそれを読み込む(なければ新規作成)
			// 指定された要素が存在しない場合、新たに要素を作成する
			// 指定された要素が存在する場合、その属性値を書き換える

			string root = document[path/*type*/].root;
			//string path = document[type].path;
			XmlDocument xdoc = document[path/*type*/].xmlDocument;
			bool isNewFile = false;

			try
			{
				xdoc.Load(path);
			}
			catch
			{
				isNewFile = true;

				XmlDeclaration declaration = xdoc.CreateXmlDeclaration("1.0", "UTF-8", null);
				try
				{
					// 宣言の行だけ書かれてる場合の対策
					xdoc.AppendChild(declaration);
				}
				catch
				{
				}

				xdoc.AppendChild(xdoc.CreateElement(root));
			}

			if (isNewFile)
			{
				// 新規ファイルの場合

				AppendItem(xdoc, name, element, value);
			}
			else
			{
				// 既存ファイルがある場合

				bool hasMatchedItem = false;	// nameと一致するname属性値を持つItem要素があるか

				if (!"".Equals(xdoc.InnerText))
				{
					foreach (XmlElement item_elem in xdoc.DocumentElement)
					{
						if (name.Equals(item_elem.GetAttribute("name")))
						{
							XmlNodeList child_nodes = item_elem.ChildNodes;
							bool hasElement = false;	// elementと一致する名前の要素があるか
							foreach (XmlNode child_node in child_nodes)
							{
								// <item>の子要素で、elementと一致する名前の要素を探す
								if (element.Equals(child_node.Name))
								{
									child_node.InnerText = value;
									hasElement = true;
								}
							}

							// elementと一致する要素がない場合は、要素を挿入して値を設定する
							if (!hasElement)
							{
								XmlElement newElement = xdoc.CreateElement(element);
								newElement.InnerText = value;
								item_elem.AppendChild(newElement);
							}

							hasMatchedItem = true;
						}
					}

					if (!hasMatchedItem)
					{
						AppendItem(xdoc, name, element, value);
					}
				}
				else
				{
					// nameと一致するname属性を持つitem要素がなければ
					// nameをname属性に持つitem要素をルート要素にアペンドして、
					// アペンドした要素の中にelementの名のアペンドして
					// element名の要素のinnerTextにvalueを設定する

					AppendItem(xdoc, name, element, value);
				}
			}

			xdoc.Save(path);
		}



		/// <summary>
		/// XMLファイルに指定された要素の属性値を書き込みます。
		/// </summary>
		/// <param name="path">XMLファイルのパス</param>
		/// <param name="name">親要素の名前</param>
		/// <param name="element">子要素</param>
		/// <param name="value">子要素の値</param>
		public void SetValue(string path/*XmlType type*/, string name, string element, int value)
		{
			ConfigManager.Instance.SetValue(path, name, element, value.ToString());
		}



		/// <summary>
		/// XMLファイルに指定された要素の属性値を書き込みます。
		/// </summary>
		/// <param name="path">XMLファイルのパス</param>
		/// <param name="name">親要素の名前</param>
		/// <param name="element">子要素</param>
		/// <param name="value">子要素の値</param>
		public void SetValue(string path/*XmlType type*/, string name, string element, bool value)
		{
			string valueStr = value ? "true" : "false";
			ConfigManager.Instance.SetValue(path, name, element, valueStr);
		}



		private void AppendItem(XmlDocument xdoc, string name, string element, string value)
		{
			XmlElement newElement = xdoc.CreateElement("item");
			newElement.SetAttribute("name", name);
			XmlElement newChildElement = xdoc.CreateElement(element);
			newChildElement.InnerText = value;
			newElement.AppendChild(newChildElement);
			xdoc.DocumentElement.AppendChild(newElement);
		}



		private void AddNewDocument(string path)
		{
			string rootname = Path.GetDirectoryName(path);

			if ("".Equals(rootname))
			{
				rootname = Path.GetFileNameWithoutExtension(path);
			}

			if (!document.ContainsKey(path))
			{
				document.Add(path, new XmlFile(rootname));
			}
		}



		private ConfigManager()
		{
			document = new Dictionary<string/*XmlType*/, XmlFile>();
			document.Add(CommonConst.XML_VIEWER/*XmlType.VIEWER*/, new XmlFile(CommonConst.XMLROOT_VIEWER/*, VIEWER_PATH*/));
			document.Add(CommonConst.XML_COMMON/*XmlType.COMMON*/, new XmlFile(CommonConst.XMLROOT_COMMON/*, COMMON_PATH*/));
		}



		public static ConfigManager Instance
		{
			get
			{
				return Holder.instance;
			}
		}



		private static class Holder
		{
			public static ConfigManager instance = new ConfigManager();
		}
	}
}