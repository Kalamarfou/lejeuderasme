using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using UltimateErasme.MenuStates;

namespace UltimateErasme
{
    class ErasmeFilesDirectoriesUtils
    {
        public static string[] dir(string directory)
        {
            string[] files;
            files = Directory.GetFileSystemEntries(directory);

            int filecount = files.GetUpperBound(0) + 1;
            string[] outFiles = new string[filecount];

            for (int i = 0; i < filecount; i++)
                outFiles[i] = (files[i].Split('.'))[0];

            return outFiles;
        }

        public static void fileDelete(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static string getTagNameValue(string pathWithFileName, string tagName)
        {
            XmlDataDocument xmldoc = new XmlDataDocument();
            xmldoc.Load(pathWithFileName);
            XmlNodeList xmlnode = xmldoc.GetElementsByTagName(tagName);

            if(xmlnode.Count == 1) {
                return xmlnode[0].FirstChild.Value;
            }
            else 
            {
                return null;
            }
        }

        public static void chargerPerso(Dictionary<String, PersoFinal> listePerso, string pathWithoutFileName, string fileName)
        {
            XmlDocument doc = new XmlDocument();
            String pathWithFileName = pathWithoutFileName + fileName + ".xml";
            doc.LoadXml(pathWithFileName);

            if (listePerso.ContainsKey(fileName))
            {
                PersoFinal persoFinal;
                listePerso.TryGetValue(fileName, out persoFinal);
                if (persoFinal == null)
                {
                    persoFinal.race = getTagNameValue(pathWithFileName, "race");
                    persoFinal.classe = getTagNameValue(pathWithFileName, "classe");
                    persoFinal.alignement = getTagNameValue(pathWithFileName, "alignement");
                    persoFinal.divinite = getTagNameValue(pathWithFileName, "divinite");
                    persoFinal.force = Int16.Parse(getTagNameValue(pathWithFileName, "force"));
                    persoFinal.constitution = Int16.Parse(getTagNameValue(pathWithFileName, "constitution"));
                    persoFinal.intelligence = Int16.Parse(getTagNameValue(pathWithFileName, "intelligence"));
                    persoFinal.dexterite = Int16.Parse(getTagNameValue(pathWithFileName, "dexterite"));
                    persoFinal.sagesse = Int16.Parse(getTagNameValue(pathWithFileName, "sagesse"));
                    persoFinal.charisme = Int16.Parse(getTagNameValue(pathWithFileName, "charisme"));
                    persoFinal.personnalite = getTagNameValue(pathWithFileName, "personnalite");
                    persoFinal.prenom = getTagNameValue(pathWithFileName, "prenom");
                    persoFinal.nom = getTagNameValue(pathWithFileName, "nom");
                    persoFinal.age = getTagNameValue(pathWithFileName, "age");
                    persoFinal.histoire = getTagNameValue(pathWithFileName, "histoire");
                    listePerso.Add(fileName, persoFinal);
                }
            }
        }

        public static Dictionary<String, PersoFinal> enregistrerPerso(Dictionary<String, PersoFinal> listePerso, PersoFinal perso, string pathWithoutFileName, bool ecraserFichier)
        {
            XmlDocument doc = new XmlDocument();
            if (ecraserFichier)
            {
                fileDelete(pathWithoutFileName + "\\" + perso.prenom + "_" + perso.nom + ".xml");
            }

            if(!listePerso.ContainsKey(string.Concat(perso.prenom, perso.nom)) || ecraserFichier) {

                XmlElement xmlPerso = doc.CreateElement("perso");
                XmlElement element = doc.CreateElement("race");
                element.InnerText = perso.race;
                xmlPerso.AppendChild(element);

                element = doc.CreateElement("classe");
                element.InnerText = perso.classe;
                xmlPerso.AppendChild(element);

                element = doc.CreateElement("alignement");
                element.InnerText = perso.alignement;
                xmlPerso.AppendChild(element);

                element = doc.CreateElement("divinite");
                element.InnerText = perso.divinite;
                xmlPerso.AppendChild(element);

                element = doc.CreateElement("force");
                element.InnerText = perso.force.ToString();
                xmlPerso.AppendChild(element);

                element = doc.CreateElement("constitution");
                element.InnerText = perso.constitution.ToString();
                xmlPerso.AppendChild(element);

                element = doc.CreateElement("intelligence");
                element.InnerText = perso.intelligence.ToString();
                xmlPerso.AppendChild(element);

                element = doc.CreateElement("dexterite");
                element.InnerText = perso.dexterite.ToString();
                xmlPerso.AppendChild(element);

                element = doc.CreateElement("sagesse");
                element.InnerText = perso.sagesse.ToString();
                xmlPerso.AppendChild(element);

                element = doc.CreateElement("charisme");
                element.InnerText = perso.charisme.ToString();
                xmlPerso.AppendChild(element);

                element = doc.CreateElement("personnalite");
                element.InnerText = perso.personnalite;
                xmlPerso.AppendChild(element);

                element = doc.CreateElement("prenom");
                element.InnerText = perso.prenom;
                xmlPerso.AppendChild(element);

                element = doc.CreateElement("nom");
                element.InnerText = perso.nom;
                xmlPerso.AppendChild(element);

                element = doc.CreateElement("age");
                element.InnerText = perso.age;
                xmlPerso.AppendChild(element);

                element = doc.CreateElement("histoire");
                element.InnerText = perso.histoire;
                xmlPerso.AppendChild(element);

                doc.Save(pathWithoutFileName + "\\" + perso.prenom + "_" + perso.nom);
            }
            return listePerso;
        }
    }
}
