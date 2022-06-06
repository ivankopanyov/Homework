.class private auto ansi beforefieldinit a
	extends [System.Runtime]System.Object
{
	// Fields
	.field private static int32 a
	.field private static int32 b
	.field private static int32 c
	.field private static bool d
	.field private static class [System.Runtime]System.Random e

	// Methods
	.method private hidebysig static 
		void a (
			string[] A_0
		) cil managed 
	{
		// Method begins at RVA 0x209c
		// Code size 349 (0x15d)
		.maxstack 4
		.entrypoint
		.locals init (
			[0] int32,
			[1] valuetype [System.Console]System.ConsoleKeyInfo,
			[2] valuetype [System.Console]System.ConsoleKey,
			[3] char
		)

		IL_0000: br IL_0132
		// loop start (head: IL_0132)
			IL_0005: ldsfld bool a::d
			IL_000a: ldc.i4.0
			IL_000b: ceq
			IL_000d: stsfld bool a::d
			IL_0012: ldstr "Current total: "
			IL_0017: ldsflda int32 a::a
			IL_001c: call instance string [System.Runtime]System.Int32::ToString()
			IL_0021: call string [System.Runtime]System.String::Concat(string, string)
			IL_0026: call void [System.Console]System.Console::WriteLine(string)
			IL_002b: ldc.i4.0
			IL_002c: stloc.00
			IL_002d: ldsfld bool a::d
			IL_0032: brfalse IL_00e6

			IL_0037: ldc.i4.5
			IL_0038: newarr [System.Runtime]System.String
			IL_003d: dup
			IL_003e: ldc.i4.0
			IL_003f: ldstr "Input number from "
			IL_0044: stelem.ref
			IL_0045: dup
			IL_0046: ldc.i4.1
			IL_0047: ldsflda int32 a::b
			IL_004c: call instance string [System.Runtime]System.Int32::ToString()
			IL_0051: stelem.ref
			IL_0052: dup
			IL_0053: ldc.i4.2
			IL_0054: ldstr " to "
			IL_0059: stelem.ref
			IL_005a: dup
			IL_005b: ldc.i4.3
			IL_005c: ldsflda int32 a::c
			IL_0061: call instance string [System.Runtime]System.Int32::ToString()
			IL_0066: stelem.ref
			IL_0067: dup
			IL_0068: ldc.i4.4
			IL_0069: ldstr ": "
			IL_006e: stelem.ref
			IL_006f: call string [System.Runtime]System.String::Concat(string[])
			IL_0074: call void [System.Console]System.Console::Write(string)
			IL_0079: br.s IL_00ce
			// loop start (head: IL_00ce)
				IL_007b: ldc.i4.1
				IL_007c: call valuetype [System.Console]System.ConsoleKeyInfo [System.Console]System.Console::ReadKey(bool)
				IL_0081: stloc.11
				IL_0082: ldloca.s 1
				IL_0084: call instance valuetype [System.Console]System.ConsoleKey [System.Console]System.ConsoleKeyInfo::get_Key()
				IL_0089: stloc.22
				IL_008a: ldloc.22
				IL_008b: ldc.i4.s 49
				IL_008d: sub
				IL_008e: switch (IL_00a9, IL_00ad, IL_00b1, IL_00b5, IL_00b9)

				IL_00a7: br.s IL_00ce

				IL_00a9: ldc.i4.1
				IL_00aa: stloc.00
				IL_00ab: br.s IL_00ce

				IL_00ad: ldc.i4.2
				IL_00ae: stloc.00
				IL_00af: br.s IL_00ce

				IL_00b1: ldc.i4.3
				IL_00b2: stloc.00
				IL_00b3: br.s IL_00ce

				IL_00b5: ldc.i4.4
				IL_00b6: stloc.00
				IL_00b7: br.s IL_00ce

				IL_00b9: ldloca.s 1
				IL_00bb: call instance char [System.Console]System.ConsoleKeyInfo::get_KeyChar()
				IL_00c0: stloc.33
				IL_00c1: ldloca.s 3
				IL_00c3: call instance string [System.Runtime]System.Char::ToString()
				IL_00c8: call int32 [System.Runtime]System.Int32::Parse(string)
				IL_00cd: stloc.00

				IL_00ce: ldloc.00
				IL_00cf: ldsfld int32 a::b
				IL_00d4: blt.s IL_007b

				IL_00d6: ldloc.00
				IL_00d7: ldsfld int32 a::c
				IL_00dc: bgt.s IL_007b
			// end loop

			IL_00de: ldloc.00
			IL_00df: call void [System.Console]System.Console::WriteLine(int32)
			IL_00e4: br.s IL_0126

			IL_00e6: ldsfld int32 a::a
			IL_00eb: ldsfld int32 a::c
			IL_00f0: ble.s IL_010a

			IL_00f2: ldsfld class [System.Runtime]System.Random a::e
			IL_00f7: ldsfld int32 a::b
			IL_00fc: ldsfld int32 a::c
			IL_0101: ldc.i4.1
			IL_0102: add
			IL_0103: callvirt instance int32 [System.Runtime]System.Random::Next(int32, int32)
			IL_0108: br.s IL_010f

			IL_010a: ldsfld int32 a::a

			IL_010f: stloc.00
			IL_0110: ldstr "Computer inputed number "
			IL_0115: ldloca.s 0
			IL_0117: call instance string [System.Runtime]System.Int32::ToString()
			IL_011c: call string [System.Runtime]System.String::Concat(string, string)
			IL_0121: call void [System.Console]System.Console::WriteLine(string)

			IL_0126: ldsfld int32 a::a
			IL_012b: ldloc.00
			IL_012c: sub
			IL_012d: stsfld int32 a::a

			IL_0132: ldsfld int32 a::a
			IL_0137: ldc.i4.0
			IL_0138: bgt IL_0005
		// end loop

		IL_013d: ldsfld bool a::d
		IL_0142: brtrue.s IL_014b

		IL_0144: ldstr "Computer win!"
		IL_0149: br.s IL_0150

		IL_014b: ldstr "You win!"

		IL_0150: call void [System.Console]System.Console::WriteLine(string)
		IL_0155: ldc.i4.1
		IL_0156: call valuetype [System.Console]System.ConsoleKeyInfo [System.Console]System.Console::ReadKey(bool)
		IL_015b: pop
		IL_015c: ret
	} // end of method a::a

	.method public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed 
	{
		// Method begins at RVA 0x2205
		// Code size 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.00
		IL_0001: call instance void [System.Runtime]System.Object::.ctor()
		IL_0006: ret
	} // end of method a::.ctor

	.method private hidebysig specialname rtspecialname static 
		void .cctor () cil managed 
	{
		// Method begins at RVA 0x220d
		// Code size 36 (0x24)
		.maxstack 8

		IL_0000: ldc.i4.s 20
		IL_0002: stsfld int32 a::a
		IL_0007: ldc.i4.1
		IL_0008: stsfld int32 a::b
		IL_000d: ldc.i4.5
		IL_000e: stsfld int32 a::c
		IL_0013: ldc.i4.0
		IL_0014: stsfld bool a::d
		IL_0019: newobj instance void [System.Runtime]System.Random::.ctor()
		IL_001e: stsfld class [System.Runtime]System.Random a::e
		IL_0023: ret
	} // end of method a::.cctor

} // end of class a
