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
    internal struct EventHandlerRegistration<ArgumentsType> where ArgumentsType : System.EventArgs
    {
        /// <summary>
        /// Gets or sets the registrator of this registration.
        /// </summary>
        public object Registrator { get; set; }

        /// <summary>
        /// Gets or sets the handler of this registration.
        /// </summary>
        public EventHandler<ArgumentsType> Handler { get; set; }

        /// <summary>
        /// Gets or sets the priority of this registration.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// GetHashCode override to create a unique hash 
        /// code from this objects members.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (this.Registrator.GetHashCode() ^ this.Handler.GetHashCode() ^ this.Priority);
        }

        /// <summary>
        /// Equals override to compare objects.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            // Ensure the passed object is of the correct type..
            if (!(obj is EventHandlerRegistration<ArgumentsType>))
                return false;

            // Compare the two handler registrations..
            var other = (EventHandlerRegistration<ArgumentsType>)obj;
            return (this.Registrator == other.Registrator &&
                    this.Handler.Equals(other.Handler));
        }

        /// <summary>
        /// Comparative operator to determine if two handler registrations are equal.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(EventHandlerRegistration<ArgumentsType> a, EventHandlerRegistration<ArgumentsType> b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Comparative operator to determine if two handler registrations are not equal.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(EventHandlerRegistration<ArgumentsType> a, EventHandlerRegistration<ArgumentsType> b)
        {
            return a.Equals(b);
        }
    }
}
