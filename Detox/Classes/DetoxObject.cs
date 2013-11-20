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

namespace Detox.Classes
{
    public class DetoxObject
    {
        /// <summary>
        /// Internal object from Terraria.
        /// </summary>
        private readonly dynamic _object;

        /// <summary>
        /// Internal Terraria type name of this object.
        /// </summary>
        private readonly string _type;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        public DetoxObject(dynamic obj, string type)
        {
            this._object = obj;
            this._type = type;
        }

        /// <summary>
        /// Gets or sets the field in this object.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public dynamic this[string name]
        {
            get { return this.GetField<dynamic>(name); }
            set { this.SetField(name, value); }
        }

        /// <summary>
        /// Obtains a field value in this object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T GetField<T>(string name)
        {
            var t = Detox.Terraria.GetType(string.Format("Terraria.{0}", this._type));
            if (t == null) return default(T);

            var field = t.GetField(name, Detox.BindFlags);
            return (field != null) ? (T)field.GetValue(this._object) : default(T);
        }

        /// <summary>
        /// Sets a field value in this object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetField<T>(string name, T value)
        {
            var t = Detox.Terraria.GetType(string.Format("Terraria.{0}", this._type));
            if (t == null) return;

            var field = t.GetField(name, Detox.BindFlags);
            if (field == null) return;

            field.SetValue(this._object, value);
        }

        /// <summary>
        /// Invokes a method from this object.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object Invoke(string name, params object[] parameters)
        {
            var t = Detox.Terraria.GetType(string.Format("Terraria.{0}", this._type));
            if (t == null) return null;

            var method = t.GetMethod(name, Detox.BindFlags);
            return (method != null) ? method.Invoke(this._object, parameters) : null;
        }
    }
}
