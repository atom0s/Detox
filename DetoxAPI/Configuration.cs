// -- Detox License ------------------------------------------------------
//
//    This file is part of Detox.
//
//    Detox is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    Detox is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with Detox.  If not, see <http://www.gnu.org/licenses/>.
// -----------------------------------------------------------------------

namespace DetoxAPI
{
    using Newtonsoft.Json;
    using System.IO;

    public static class Configuration
    {
        /// <summary>
        /// Deserializes a configuration file back to the given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file"></param>
        /// <returns></returns>
        public static T ReadConfiguration<T>(string file)
        {
            if (!File.Exists(file))
                return default(T);

            Stream fStream = null;

            try
            {
                fStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
                using (var reader = new StreamReader(fStream))
                {
                    fStream = null;
                    return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
                }
            }
            finally
            {
                if (fStream != null)
                    fStream.Dispose();
            }
        }

        /// <summary>
        /// Serializes the given object to a configuration file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool WriteConfiguration<T>(string file, T data)
        {
            Stream fStream = null;

            try
            {
                fStream = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.Write);
                var serializedConfig = JsonConvert.SerializeObject(data, Formatting.Indented);
                using (var writer = new StreamWriter(fStream))
                {
                    fStream = null;
                    writer.Write(serializedConfig);
                    writer.Flush();
                    return true;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                if (fStream != null)
                    fStream.Dispose();
            }
        }
    }
}
