What is Detox?
=====
> Detox is a client modification for Terraria that extends the base of what the game can do and adds a layer of customization to the client via the use of plugins. Detox does not modify the client executable that is on disk and does not rely on a specific version of the Terraria.exe, this means that Detox is almost guaranteed to work for any version of Terraria.

> Detox makes use of Mono.Cecil, which allows it to modify the Terraria.exe in memory and run the modifications at runtime without having to overwrite the original game executable. Using this allows Detox to interact with Terraria in a more clean manner. It also means that Detox can work with other modifications, assuming they do not alter the ILCode of some of the functions it uses pattern scanning for!

> While using Detox, plugin authors can access every part of Terraria. Thus allowing you to extend, alter, recreate, or even remove any part of the game you want. You can virtually recreate the entire game through your own IL edits from your own plugin.

What can I do with Detox?
=====
> The best way to answer this is; virtually anything!
> You can alter any part of the game with a little bit of time and creativity. (Coding knowledge is required though!)

> Detox creates a bridge between the actual Terraria client and itself, allowing users to create and load plugins that grant full access to the game. You can alter npc's, players, items, buffs, etc. all with a little bit of work and time. Along with that, Detox also has a built in UI system that you can use and interact with from plugins! So no longer does everything have to be command based, you can create a simple UI system for anything you want and make things even easier and enjoyable to use!

Does this alter my game or require any extra work?
=====
> Not at all, Detox uses Mono.Cecil as described above. This allows Detox to load, modify, and run Terraria all from memory without ever saving the modifications to disk. This means your original Terraria.exe will never be overwritten, and you will never need to replace it. If you ever feel like not using Detox, just run Terraria as normal; it's as simple as that!

> Using Mono.Cecil gives Detox the advantage of not needing updates on a constant basis as Terraria updates. While using this method greatly increases the chances of never needing to update Detox when Terraria updates, it is not 100% "update-proof". Detox does use IL pattern scanning to locate some areas where it makes patches. Because of this, the IL code can change between versions and cause Detox to not work. But the likely-hood of it is slim.

Legal Info
=====
> Terraria (c) Redigit / www.terraria.org

> I, atom0s, claim no ownership to the trademark Terraria or any of its content, data, images, etc. All rights reserved to their rightful and original owners.

> Detox is free software: you can redistribute it and/or modify
> it under the terms of the GNU General Public License as published by
> the Free Software Foundation, either version 3 of the License, or
> (at your option) any later version.

> Detox is distributed in the hope that it will be useful,
> but WITHOUT ANY WARRANTY; without even the implied warranty of
> MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
> GNU General Public License for more details.

Credits and Thanks
=====
Thanks to the following projects and people for their part in Detox:
* **Project References**
 * Mono.Cecil (http://www.mono-project.com/Cecil)
 * Mono.Cecil.Rocks (http://www.mono-project.com/Cecil)
 * NeoforceControls (https://github.com/NeoforceControls/XNA)
 * Newtonsoft.Json (http://james.newtonking.com/json)
 * Terraria Custom Content Loader (Terraria skin for Neoforce) (http://www.terrariaonline.com/threads/terraria-custom-content-loader.44541/)
 * XNA Game Studio 4.0 (http://www.microsoft.com/en-us/download/details.aspx?id=23714)
* **Code / Resource Usage**
 * ADRMod - Inventory background and default fonts.
 * IconFinder - Detox icon from their free repo. (http://www.iconfinder.com)
 * TShock - Event management system is based on their hook system.
* **Individual Thanks**
 * MarioE - For giving me ADRMod a long while ago.
 * Zidonuke - Terraria packet documentation xls.

If I missed anyone or anything; let me know!
