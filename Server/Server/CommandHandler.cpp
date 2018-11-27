#include "CommandHandler.h"

CommandHandler::CommandHandler(QObject *parent) : QObject(parent)
{
	// Create pointers to all commands 
	comm1 = new Comm1();
}

bool CommandHandler::generate_commands(std::vector<Command*> &command_queue, Msg *msg)
{
	bool cmdChck = parse_buffor(msg);
	if (!cmdChck) return true;
	else {
		dispatch(command_queue);  // fills command queue
		return false;
	}
}

bool CommandHandler::parse_buffor(Msg *msg) 
{
	//TODO look for command in buffor;
	return false;
}

void CommandHandler::dispatch_commands(std::vector<Command*> &command_queue)
{
	std::map<int, Command*>::iterator iter;
	for (iter = commands.begin(); iter != commands.end(); iter++) {
		if (iter->first->get_input_type() == ACTION)
			command_queue.push_back(iter->second);
	}
}

CommandHandler::~CommandHandler()
{
	// Delete all command pointers    
	std::map<int, Command*>::iterator iter;
	for (iter = commands.begin(); iter != commands.end(); iter++)
		delete iter->second;
}


