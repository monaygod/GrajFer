#include "ServerGrajFer.h"



ServerGrajFer::ServerGrajFer()
{
	game_server = new GameServer();
}


ServerGrajFer::~ServerGrajFer()
{
}

void ServerGrajFer::initServer()
{
	game_server->startServer(this);
}
