<Query Kind="Program">
  <NuGetReference>Dapper</NuGetReference>
  <NuGetReference>MiniExcel</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>System.Data.SqlClient</NuGetReference>
  <Namespace>Dapper</Namespace>
  <Namespace>MiniExcelLibs</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.IO.Compression</Namespace>
  <RemoveNamespace>System.Data</RemoveNamespace>
  <RemoveNamespace>System.Diagnostics</RemoveNamespace>
  <RemoveNamespace>System.Linq.Expressions</RemoveNamespace>
  <RemoveNamespace>System.Text.RegularExpressions</RemoveNamespace>
  <RemoveNamespace>System.Threading</RemoveNamespace>
  <RemoveNamespace>System.Transactions</RemoveNamespace>
</Query>

void Main(){

	var xml2 = GetSheet1Xml(@"D:\git\MiniExcel\samples\xlsx\CloseXml_InsertCellValues\CloseXml_InsertCellValues.xlsx");

	var doc = XDocument.Parse(xml2);

	XmlNamespaceManager ns = new XmlNamespaceManager(new NameTable());
	ns.AddNamespace("x", "http://schemas.openxmlformats.org/spreadsheetml/2006/main");

	Console.WriteLine(RemoveAllNamespaces(doc.ToString()));
}

internal static string GetSheet1Xml(string path)
{
	var ns = new XmlNamespaceManager(new NameTable());
	ns.AddNamespace("x", "http://schemas.openxmlformats.org/spreadsheetml/2006/main");
	string refV;
	using (var stream = File.OpenRead(path))
	using (ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Read, false, Encoding.UTF8))
	{
		var sheet = archive.Entries.Single(w => w.FullName.StartsWith("xl/worksheets/sheet1", StringComparison.OrdinalIgnoreCase)
		   || w.FullName.StartsWith("/xl/worksheets/sheet1", StringComparison.OrdinalIgnoreCase)
		);
		using (var sheetStream = sheet.Open())
		{
			var doc = new XmlDocument();
			doc.Load(sheetStream);
			return doc.OuterXml.ToString();
		}
	}

	return refV;
}

//Implemented based on interface, not part of algorithm
public static string RemoveAllNamespaces(string xmlDocument)
{
	XElement xmlDocumentWithoutNs = RemoveAllNamespaces(XElement.Parse(xmlDocument));

	return xmlDocumentWithoutNs.ToString();
}

//Core recursion function
private static XElement RemoveAllNamespaces(XElement xmlDocument)
{
	if (!xmlDocument.HasElements)
	{
		XElement xElement = new XElement(xmlDocument.Name.LocalName);
		xElement.Value = xmlDocument.Value;

		foreach (XAttribute attribute in xmlDocument.Attributes())
			xElement.Add(attribute);

		return xElement;
	}
	return new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(el => RemoveAllNamespaces(el)));
}

void Main2()
{
	var doc = XDocument.Parse(xml);


	XmlNamespaceManager ns = new XmlNamespaceManager(new NameTable());
	ns.AddNamespace("x", "http://schemas.openxmlformats.org/spreadsheetml/2006/main");





	var dimension = doc.XPathSelectElement("/x:worksheet/x:dimension", ns);
	Console.WriteLine(dimension); //<dimension ref="A1:B100" xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" />
	
	//WriteElement(dimension);
	dimension.Name=dimension.Name.LocalName;
	Console.WriteLine(dimension); //<dimension ref="A1:B100" />
	
	XmlDocument xmlDocument = new XmlDocument();
	
	XmlElement folioNode = xmlDocument.CreateElement("x", "dimension", "x"); //<x:dimension xmlns:x="x" />
	Console.WriteLine(folioNode);

	//Console.WriteLine(doc);

	XContainer container = XElement.Parse(xml);
	
	var x = XNamespace.Get("http://schemas.openxmlformats.org/spreadsheetml/2006/main") ;


	XElement xmlTree = new XElement(x + "Item");
	Console.WriteLine(xmlTree);
}

private void PushAncestors(XElement e)
{
	while (true)
	{
		e = e.Parent as XElement;
		if (e == null)
		{
			break;
		}
		XAttribute xAttribute = e.LastAttribute;
		if (xAttribute == null)
		{
			continue;
		}
		do
		{
			xAttribute = xAttribute.NextAttribute;
			//if (xAttribute.IsNamespaceDeclaration)
			//{
			//	_resolver.AddFirst((xAttribute.Name.NamespaceName.Length == 0) ? string.Empty : xAttribute.Name.LocalName, XNamespace.Get(xAttribute.Value));
			//}
		}
		while (xAttribute != e.LastAttribute);
	}
}

//public string WriteElement(XElement e)
//{
//	var sb = new StringBuilder();
//	PushAncestors(e);
//	XElement xElement = e;
//	XNode xNode = e;
//	while (true)
//	{
//		e = xNode as XElement;
//		if (e != null)
//		{
//			WriteStartElement(e);
//			if (e.content == null)
//			{
//				WriteEndElement();
//			}
//			else
//			{
//				string text = e.content as string;
//				if (text == null)
//				{
//					xNode = ((XNode)e.content).next;
//					continue;
//				}
//				_writer.WriteString(text);
//				WriteFullEndElement();
//			}
//		}
//		else
//		{
//			xNode.WriteTo(_writer);
//		}
//		while (xNode != xElement && xNode == xNode.parent.content)
//		{
//			xNode = xNode.parent;
//			WriteFullEndElement();
//		}
//		if (xNode != xElement)
//		{
//			xNode = xNode.next;
//			continue;
//		}
//		break;
//	}
//}

// You can define other methods, fields, classes and namespaces here
const string xml =@"<?xml version=""1.0"" encoding=""utf-8""?>
<x:worksheet xmlns:r=""http://schemas.openxmlformats.org/officeDocument/2006/relationships""
    xmlns:x=""http://schemas.openxmlformats.org/spreadsheetml/2006/main"">
    <x:sheetPr>
        <x:outlinePr summaryBelow=""1"" summaryRight=""1"" />
    </x:sheetPr>
    <x:dimension ref=""A1:B2"" />
    <x:sheetViews>
        <x:sheetView workbookViewId=""0"" />
    </x:sheetViews>
    <x:sheetFormatPr defaultRowHeight=""15"" />
    <x:sheetData>
        <x:row r=""1"" spans=""1:2"">
            <x:c r=""A1"" s=""0"" t=""s"">
                <x:v>0</x:v>
            </x:c>
            <x:c r=""B1"" s=""1"">
                <x:v>44257.3802667361</x:v>
            </x:c>
        </x:row>
        <x:row r=""2"" spans=""1:2"">
            <x:c r=""A2"" s=""0"">
                <x:f>MID(A1, 7, 5)</x:f>
            </x:c>
            <x:c r=""B2"" s=""0"" t=""n"">
                <x:v>123</x:v>
            </x:c>
        </x:row>
    </x:sheetData>
    <x:printOptions horizontalCentered=""0"" verticalCentered=""0"" headings=""0"" gridLines=""0"" />
    <x:pageMargins left=""0.75"" right=""0.75"" top=""0.75"" bottom=""0.5"" header=""0.5"" footer=""0.75"" />
    <x:pageSetup paperSize=""1"" scale=""100"" pageOrder=""downThenOver"" orientation=""default"" blackAndWhite=""0"" draft=""0"" cellComments=""none"" errors=""displayed"" />
    <x:headerFooter />
    <x:tableParts count=""0"" />
</x:worksheet>";