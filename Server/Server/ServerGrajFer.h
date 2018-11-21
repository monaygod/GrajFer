#pragma once
#include <QObject>
#include "GameServer.h"
#include "CommandHandler.h"

class ServerGrajFer : public QObject
{
	Q_OBJECT
private:
	GameServer *game_server;
	CommandHandler *command_handler;

public:
	ServerGrajFer();
	~ServerGrajFer();
	void initServer();

};

