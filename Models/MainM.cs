namespace Reformers.Models
{
    internal class MainM
    {
        public ObservableCollection<item> Lists = new ObservableCollection<item>();

        public ObservableCollection<itemGroup> Groups=new ObservableCollection<itemGroup>();

        public void Processing(string filePath)
        {
            Lists.Clear();
            Groups.Clear();

            var path = new System.IO.FileInfo(filePath);

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filePath);
            
            XmlElement? xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                foreach (XmlElement xnode in xRoot)
                {
                    XmlNode? name = xnode.Attributes.GetNamedItem("name");
                    XmlNode? group = xnode.Attributes.GetNamedItem("group");
                    Lists.Add(new item() { name = name?.Value, group = group?.Value });
                }

                XmlAttribute attr = xDoc.CreateAttribute("item");
                attr.Value = Lists.Count.ToString();

                xDoc.DocumentElement.SetAttributeNode(attr);

                xDoc.Save(filePath);
            }

            var grou = Lists.GroupBy(x => x.group);
            XDocument xdoc = new XDocument();

            XElement people = new XElement("groups");
            
            foreach (var item in grou)
            {
                
                XElement tom = new XElement("group");
                XAttribute tomNameAttr = new XAttribute("name", item.Key);
                tom.Add(tomNameAttr);
                var temp = new itemGroup("group - "+item.Key);
                foreach (var item1 in item)
                {
                    temp.StreetList.Add(new itemGroup("item - " + item1.name));
                    XElement tomCompanyElem = new XElement("item");
                    XAttribute tomNameAttr1 = new XAttribute("name", item1.name);
                    tomCompanyElem.Add(tomNameAttr1);
                    tom.Add(tomCompanyElem);
                }
                temp.CityName += " item="+ item.Count();
                Groups.Add(temp);
                XAttribute tomNameAttr3 = new XAttribute("item", item.Count());
                tom.Add(tomNameAttr3);


                people.Add(tom);
            }

            xdoc.Add(people);
            xdoc.Save(path.DirectoryName + "\\Groups.xml");
        }
    }
}
