#pragma once

#include <map>
#include <vector>
#include "command_constants.h"

class Command
{
public:
	virtual ~Command() {}
	virtual void execute(Msg *msg) = 0; //how do i into...
	virtual Type get_type() = 0;
};

class CommandHandler : public QObject //dlaczego dziedziczymy po tym obiekcie?
{
private:
	// Pointers to all commands
	Command *comm1;

	std::map <int, Command*> commands;
	void dispatch_commands(std::vector<Command*> &command_queue);

public:
	CommandHandler(QObject *parent);
	~CommandHandler();
	bool parse_buffor(Msg *msg);
	bool generate_commands(std::vector<Command*> &command_queue, Msg *msg);
};

class Comm1 : public Command
{
public:
	void execute(Msg *msg) { Msg->comm1(); }
	CommandType get_type() {};
};
