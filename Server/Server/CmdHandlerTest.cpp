#include "CmdHandlerTest.h"

CmdHandlerTest::CmdHandlerTest()
{
	exit = false;
	// Initialize input handler
	command_handler = new CommandHandler();
	// Take some buffor
	msg = new Msg;
	// Command queue
	std::vector<Command*> command_queue;
}

void CmdHandlerTest::execute()
{
	while (!exit) {
		// Handle msg
		exit = command_handler->generate_commands(command_queue);
		update();
	}
}

void CmdHandlerTest::update()
{
	// Update queue
	while (!command_queue.empty()) {
		command_queue.back()->execute(msg);
		command_queue.pop_back();
	}
}