using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
namespace SQA_Tower_Defense
{
    public class LanguageManager
    {

        private class LMap
        {
            ArrayList keys;
            ArrayList items;

            public LMap()
            {
                keys = new ArrayList();
                items = new ArrayList();
            }

            public Object get(String key)
            {
                return items.ToArray()[keys.IndexOf(key)];
            }

            public Boolean contains(String key)
            {
                return keys.Contains(key);
            }

            public void set(String key, Object item)
           {
               if (!keys.Contains(key))
               {
                   keys.Insert(keys.Count, key);
                   items.Insert(items.Count, item);
               }
               else
               {
                   int i = keys.IndexOf(key);
                   keys.Remove(key);
                   items.RemoveAt(i);
                   set(key, item);
               }
           }

            public Boolean Add(String key, Object item)
            {
                if (keys.Contains(key))
                {
                    return false;
                }

                else
                {
                    set(key, item);
                    return true;
                }
            }
        }

         LMap holder;

        public LanguageManager()
        {
            holder = new LMap();
            ReadAllFiles();
        }

        public Boolean HasPhrase (String p)
        {
            return holder.contains(p); 
        }

        public void addPhrase(String phrase)
        {
            holder.Add(phrase, new LMap());
        }

        public void addTranslation(String phrase, String lang, String translation)
        {
            LMap L = (LMap)holder.get(phrase);
            L.Add(lang, translation);
            holder.set(phrase, L);
        }

        public String getTranslation(String phrase, String lang)
        {
            LMap L = (LMap)holder.get(phrase);
            return (String)L.get(lang);
        }

        public void ReadAllFiles()
        {
            string[] fn = Directory.GetFiles("Resources");
            for (int x = 0; x < fn.Length; x++)
            {
                ReadFile(fn[x]);
            }
        }

        public void ReadFile(String fileName)
        {
            string[] lines = System.IO.File.ReadAllLines(fileName);
            FileStream fs = new FileStream(fileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            for (int i = 1; i < lines.Length; i = i + 2)
            {
                if (!holder.contains(lines[i]))
                {
                    addPhrase(lines[i]);
                    addTranslation(lines[i], lines[0], lines[i + 1]);
                }
                else
                {
                    addTranslation(lines[i], lines[0], lines[i + 1]);
                }

            }

            fs.Close();
            sr.Close();


        }


    }
}

