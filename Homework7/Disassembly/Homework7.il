.assembly Homework7
{
	.custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilationRelaxationsAttribute::.ctor(int32) = (
		01 00 08 00 00 00 00 00
	)
	.custom instance void [System.Runtime]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::.ctor() = (
		01 00 01 00 54 02 16 57 72 61 70 4e 6f 6e 45 78
		63 65 70 74 69 6f 6e 54 68 72 6f 77 73 01
	)
	.custom instance void [System.Runtime]System.Diagnostics.DebuggableAttribute::.ctor(valuetype [System.Runtime]System.Diagnostics.DebuggableAttribute/DebuggingModes) = (
		01 00 02 00 00 00 00 00
	)
	.custom instance void [System.Runtime]System.Runtime.Versioning.TargetFrameworkAttribute::.ctor(string) = (
		01 00 18 2e 4e 45 54 43 6f 72 65 41 70 70 2c 56
		65 72 73 69 6f 6e 3d 76 35 2e 30 01 00 54 0e 14
		46 72 61 6d 65 77 6f 72 6b 44 69 73 70 6c 61 79
		4e 61 6d 65 00
	)
	.custom instance void [System.Runtime]System.Reflection.AssemblyCompanyAttribute::.ctor(string) = (
		01 00 09 48 6f 6d 65 77 6f 72 6b 37 00 00
	)
	.custom instance void [System.Runtime]System.Reflection.AssemblyConfigurationAttribute::.ctor(string) = (
		01 00 07 52 65 6c 65 61 73 65 00 00
	)
	.custom instance void [System.Runtime]System.Reflection.AssemblyFileVersionAttribute::.ctor(string) = (
		01 00 07 31 2e 30 2e 30 2e 30 00 00
	)
	.custom instance void [System.Runtime]System.Reflection.AssemblyInformationalVersionAttribute::.ctor(string) = (
		01 00 05 31 2e 30 2e 30 00 00
	)
	.custom instance void [System.Runtime]System.Reflection.AssemblyProductAttribute::.ctor(string) = (
		01 00 09 48 6f 6d 65 77 6f 72 6b 37 00 00
	)
	.custom instance void [System.Runtime]System.Reflection.AssemblyTitleAttribute::.ctor(string) = (
		01 00 09 48 6f 6d 65 77 6f 72 6b 37 00 00
	)
	.hash algorithm 0x00008004 // SHA1
	.ver 1:0:0:0
}

.class private auto ansi beforefieldinit Homework7.Program
	extends [System.Runtime]System.Object
{
	// Fields
	.field private static int32 total
	.field private static int32 min
	.field private static int32 max
	.field private static bool isPlayer
	.field private static class [System.Runtime]System.Random random

	// Methods
	.method private hidebysig static 
		void Main (
			string[] args
		) cil managed 
	{
		// Method begins at RVA 0x2050
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
			IL_0005: ldsfld bool Homework7.Program::isPlayer
			IL_000a: ldc.i4.0
			IL_000b: ceq
			IL_000d: stsfld bool Homework7.Program::isPlayer
			IL_0012: ldstr "Current total: "
			IL_0017: ldsflda int32 Homework7.Program::total
			IL_001c: call instance string [System.Runtime]System.Int32::ToString()
			IL_0021: call string [System.Runtime]System.String::Concat(string, string)
			IL_0026: call void [System.Console]System.Console::WriteLine(string)
			IL_002b: ldc.i4.0
			// IL_002c: stloc.00
			// Исправление ошибки декомпиляции
			IL_002c: stloc.0
			IL_002d: ldsfld bool Homework7.Program::isPlayer
			IL_0032: brfalse IL_00e6

			IL_0037: ldc.i4.5
			IL_0038: newarr [System.Runtime]System.String
			IL_003d: dup
			IL_003e: ldc.i4.0
			IL_003f: ldstr "Input number from "
			IL_0044: stelem.ref
			IL_0045: dup
			IL_0046: ldc.i4.1
			IL_0047: ldsflda int32 Homework7.Program::min
			IL_004c: call instance string [System.Runtime]System.Int32::ToString()
			IL_0051: stelem.ref
			IL_0052: dup
			IL_0053: ldc.i4.2
			IL_0054: ldstr " to "
			IL_0059: stelem.ref
			IL_005a: dup
			IL_005b: ldc.i4.3
			IL_005c: ldsflda int32 Homework7.Program::max
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
				// IL_0081: stloc.11
				// Исправление ошибки декомпиляции
				IL_0081: stloc.1
				IL_0082: ldloca.s 1
				IL_0084: call instance valuetype [System.Console]System.ConsoleKey [System.Console]System.ConsoleKeyInfo::get_Key()
				// IL_0089: stloc.22
				// Исправление ошибки декомпиляции
				IL_0089: stloc.2
				// IL_008a: ldloc.22
				// Исправление ошибки декомпиляции
				IL_008a: ldloc.2
				IL_008b: ldc.i4.s 49
				IL_008d: sub
				// IL_008e: switch (IL_00a9, IL_00ad, IL_00b1, IL_00b5, IL_00b9)
				// Все кейсы свича теперь ссылаются на инструкцию последнего кейса
				IL_008e: switch (IL_00b9, IL_00b9, IL_00b9, IL_00b9, IL_00b9)

				IL_00a7: br.s IL_00ce

				IL_00a9: ldc.i4.1
				// IL_00aa: stloc.00
				// Исправление ошибки декомпиляции
				IL_00aa: stloc.0
				IL_00ab: br.s IL_00ce

				IL_00ad: ldc.i4.2
				// IL_00ae: stloc.00
				// Исправление ошибки декомпиляции
				IL_00ae: stloc.0
				IL_00af: br.s IL_00ce

				IL_00b1: ldc.i4.3
				// IL_00b2: stloc.00
				// Исправление ошибки декомпиляции
				IL_00b2: stloc.0
				IL_00b3: br.s IL_00ce

				IL_00b5: ldc.i4.4
				// IL_00b6: stloc.00
				// Исправление ошибки декомпиляции
				IL_00b6: stloc.0
				IL_00b7: br.s IL_00ce

				IL_00b9: ldloca.s 1
				IL_00bb: call instance char [System.Console]System.ConsoleKeyInfo::get_KeyChar()
				// IL_00c0: stloc.33
				// Исправление ошибки декомпиляции
				IL_00c0: stloc.3
				IL_00c1: ldloca.s 3
				IL_00c3: call instance string [System.Runtime]System.Char::ToString()
				IL_00c8: call int32 [System.Runtime]System.Int32::Parse(string)
				// IL_00cd: stloc.00
				// Исправление ошибки декомпиляции
				IL_00cd: stloc.0

				// IL_00ce: ldloc.00
				// Исправление ошибки декомпиляции
				IL_00ce: ldloc.0
				IL_00cf: ldsfld int32 Homework7.Program::min
				IL_00d4: blt.s IL_007b

				// IL_00d6: ldloc.00
				// Исправление ошибки декомпиляции
				IL_00d6: ldloc.0
				IL_00d7: ldsfld int32 Homework7.Program::max
				IL_00dc: bgt.s IL_007b
			// end loop

			// IL_00de: ldloc.00
			IL_00de: ldloc.0
			IL_00df: call void [System.Console]System.Console::WriteLine(int32)
			IL_00e4: br.s IL_0126

			IL_00e6: ldsfld int32 Homework7.Program::total
			IL_00eb: ldsfld int32 Homework7.Program::max
			IL_00f0: ble.s IL_010a

			IL_00f2: ldsfld class [System.Runtime]System.Random Homework7.Program::random
			IL_00f7: ldsfld int32 Homework7.Program::min
			IL_00fc: ldsfld int32 Homework7.Program::max
			IL_0101: ldc.i4.1
			IL_0102: add
			IL_0103: callvirt instance int32 [System.Runtime]System.Random::Next(int32, int32)
			IL_0108: br.s IL_010f

			IL_010a: ldsfld int32 Homework7.Program::total

			// IL_010f: stloc.00
			// Исправление ошибки декомпиляции
			IL_010f: stloc.0
			IL_0110: ldstr "Computer inputed number "
			IL_0115: ldloca.s 0
			IL_0117: call instance string [System.Runtime]System.Int32::ToString()
			IL_011c: call string [System.Runtime]System.String::Concat(string, string)
			IL_0121: call void [System.Console]System.Console::WriteLine(string)

			IL_0126: ldsfld int32 Homework7.Program::total
			// IL_012b: ldloc.00
			// Исправление ошибки декомпиляции
			IL_012b: ldloc.0
			IL_012c: sub
			IL_012d: stsfld int32 Homework7.Program::total

			IL_0132: ldsfld int32 Homework7.Program::total
			IL_0137: ldc.i4.0
			IL_0138: bgt IL_0005
		// end loop

		IL_013d: ldsfld bool Homework7.Program::isPlayer
		IL_0142: brtrue.s IL_014b

		IL_0144: ldstr "Computer win!"
		IL_0149: br.s IL_0150

		// IL_014b: ldstr "You win!"
		// Добавляем поздравление пользователя
		IL_014b: ldstr "You win! Congratulations!"

		IL_0150: call void [System.Console]System.Console::WriteLine(string)
		IL_0155: ldc.i4.1
		IL_0156: call valuetype [System.Console]System.ConsoleKeyInfo [System.Console]System.Console::ReadKey(bool)
		IL_015b: pop
		IL_015c: ret
	} // end of method Program::Main

	.method public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed 
	{
		// Method begins at RVA 0x21b9
		// Code size 7 (0x7)
		.maxstack 8

		// IL_0000: ldarg.00
		// Исправление ошибки декомпиляции
		IL_0000: ldarg.0
		IL_0001: call instance void [System.Runtime]System.Object::.ctor()
		IL_0006: ret
	} // end of method Program::.ctor

	.method private hidebysig specialname rtspecialname static 
		void .cctor () cil managed 
	{
		// Method begins at RVA 0x21c1
		// Code size 36 (0x24)
		.maxstack 8

		// IL_0000: ldc.i4.s 20
		// Увеличиваем значение total
		IL_0000: ldc.i4.s 30
		IL_0002: stsfld int32 Homework7.Program::total
		IL_0007: ldc.i4.1
		IL_0008: stsfld int32 Homework7.Program::min
		IL_000d: ldc.i4.5
		IL_000e: stsfld int32 Homework7.Program::max
		IL_0013: ldc.i4.0
		IL_0014: stsfld bool Homework7.Program::isPlayer
		IL_0019: newobj instance void [System.Runtime]System.Random::.ctor()
		IL_001e: stsfld class [System.Runtime]System.Random Homework7.Program::random
		IL_0023: ret
	} // end of method Program::.cctor

} // end of class Homework7.Program
