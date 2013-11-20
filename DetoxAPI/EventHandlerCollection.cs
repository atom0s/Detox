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
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    public sealed class EventHandlerCollection<ArgumentsType> : IEnumerable<EventHandlerRegistration<ArgumentsType>> where ArgumentsType : System.EventArgs
    {
        /// <summary>
        /// Internal list of registrations.
        /// </summary>
        private List<EventHandlerRegistration<ArgumentsType>> _registrations;

        /// <summary>
        /// Lockable object to prevent threading issues.
        /// </summary>
        private readonly object _registrationsLock = new object();

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="name"></param>
        internal EventHandlerCollection(string name)
        {
            this._registrations = new List<EventHandlerRegistration<ArgumentsType>>();
            this.EventName = name;
        }

        /// <summary>
        /// Registers a new handler to this event.
        /// </summary>
        /// <param name="registrator"></param>
        /// <param name="handler"></param>
        /// <param name="priority"></param>
        public void Register(object registrator, EventHandler<ArgumentsType> handler, int priority)
        {
            // Ensure the priority is valid..
            if (registrator != null && priority == 0)
                throw new InvalidOperationException("Cannot register a new event with a priority of 0.");

            // Create the new registration object..
            var registration = new EventHandlerRegistration<ArgumentsType>
                {
                    Handler = handler,
                    Priority = priority,
                    Registrator = registrator
                };

            lock (this._registrationsLock)
            {
                // Create clone of current registrations to manipulate..
                var registrations = new List<EventHandlerRegistration<ArgumentsType>>(this._registrations.Count);
                registrations.AddRange(this._registrations);

                // Locate the proper priority position..
                var insertIndex = registrations.Count;
                for (var x = 0; x < registrations.Count; x++)
                {
                    if (registrations[x].Priority > priority)
                    {
                        insertIndex = x;
                        break;
                    }
                }

                // Insert the new registration at the found index..
                registrations.Insert(insertIndex, registration);
                Interlocked.Exchange(ref this._registrations, registrations);
            }
        }

        /// <summary>
        /// Deregisters an existing handler from this event.
        /// </summary>
        /// <param name="registrator"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public bool Deregister(object registrator, EventHandler<ArgumentsType> handler)
        {
            // Create the registration object to locate..
            var registration = new EventHandlerRegistration<ArgumentsType>
                {
                    Handler = handler,
                    Registrator = registrator
                };

            lock (this._registrationsLock)
            {
                // Attempt to locate the registration..
                var registrationIndex = this._registrations.IndexOf(registration);
                if (registrationIndex == -1)
                    return false;

                // Create clone of current registrations to manipulate..
                var registrations = new List<EventHandlerRegistration<ArgumentsType>>(this._registrations.Count);
                registrations.AddRange(this._registrations.Where((t, x) => x != registrationIndex));

                // Update the registrations..
                Interlocked.Exchange(ref this._registrations, registrations);
            }

            return true;
        }

        /// <summary>
        /// Invokes this event.
        /// </summary>
        /// <param name="args"></param>
        public void Invoke(ArgumentsType args)
        {
            // Ensure we have something to invoke..
            if (this._registrations.Count == 0)
                return;

            // Invoke each handler..
            foreach (var r in this._registrations)
            {
                try
                {
                    r.Handler(args);
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Returns the enumerator for our registration list.
        /// </summary>
        /// <returns></returns>
        IEnumerator<EventHandlerRegistration<ArgumentsType>> IEnumerable<EventHandlerRegistration<ArgumentsType>>.GetEnumerator()
        {
            return this._registrations.GetEnumerator();
        }

        /// <summary>
        /// Returns the enumerator for our registration list.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._registrations.GetEnumerator();
        }

        /// <summary>
        /// Gets or sets the event name associated with this collection.
        /// </summary>
        public string EventName { get; set; }
    }
}
