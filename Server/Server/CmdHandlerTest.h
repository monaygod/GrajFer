#pragma once
#include "CommandHandler.h"

class Msg
{
public:
	uint;
};

class CmdHandlerTest
{
private:
	bool exit;
	CommandHandler *command_handler;
	Msg *msg;
	void update();

public:
	void execute();
};