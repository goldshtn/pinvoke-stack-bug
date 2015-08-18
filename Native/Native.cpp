#include <stdio.h>

class Frobber
{
private:
	char const* m_FrobName;
	int m_FrobId;
public:
	__declspec(noinline) Frobber(char const* FrobName)
		: m_FrobName(FrobName), m_FrobId(42)
	{
	}
	__declspec(noinline) void FrobIt()
	{
		printf("Frobbing %c (id %d)\n", m_FrobName[0], m_FrobId);
	}
};

enum OPCODE
{
	OPCODE_FROB,
	OPCODE_FROB_WITH_CB
};

#pragma pack(4)
struct CALLBACK_OUTPUT
{
	__int32* ErrorCodeArray;
	unsigned __int64 NumberOfErrors;
};

typedef __int32(__stdcall *FROB_WIDGET_CALLBACK)(
	__int32 OperationCode,
	CALLBACK_OUTPUT* CallbackOutput
	);

extern "C" __declspec(dllexport) void __stdcall FrobWidgetWithCallback(
	char const* FrobName,
	FROB_WIDGET_CALLBACK Callback
	)
{
	CALLBACK_OUTPUT CallbackOutput = { 0 };
	Frobber frobber(FrobName);
	if (0 == Callback(OPCODE_FROB_WITH_CB, &CallbackOutput))
	{
		frobber.FrobIt();
	}
}