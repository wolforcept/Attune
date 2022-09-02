using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attune.Programs.Keyboard
{

    [Serializable]
    internal class MacroData
    {
        public string macroString;
        public string colorString;

        public Color getColor()
        {
            return Color.FromArgb(int.Parse(colorString, System.Globalization.NumberStyles.HexNumber));
        }
    }

    [Serializable]
    internal class KeyboardProgramData
    {

        public MacroData[] macros = new MacroData[64];

        private const string saveLocation = "./macros.json";

        KeyboardProgramData()
        {
            for (int i = 0; i < 64; i++)
            {
                setMacro(i, "", "FF222222");
            }
        }

        public void setMacro(int padNr, string macroString, string colorString)
        {
            macros[padNr] = new MacroData { macroString = macroString, colorString = colorString };
        }

        internal MacroData getMacro(int i)
        {
            return macros[i];
        }

        internal void save()
        {
            WriteToJsonFile(saveLocation, this);
        }

        internal static KeyboardProgramData load()
        {
            return ReadFromJsonFile(saveLocation);
        }

        //
        //

        /// <summary>
        /// Writes the given object instance to a Json file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
        /// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [JsonIgnore] attribute.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToJsonFile(string filePath, KeyboardProgramData objectToWrite, bool append = false)
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        /// <summary>
        /// Reads an object instance from an Json file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object to read from the file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the Json file.</returns>
        public static KeyboardProgramData ReadFromJsonFile(string filePath)
        {
            if (!File.Exists(filePath))
                return new KeyboardProgramData();

            TextReader reader = null;
            try
            {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<KeyboardProgramData>(fileContents);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
    }
}
