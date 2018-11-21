#include "GameServer.h"
#include <iostream>

GameServer::GameServer(QObject *parent) :
	QTcpServer(parent)
{
}

void GameServer::startServer(ServerGrajFer *_server)
{
	main_server = _server;
	int port = 1234;

	if (!this->listen(QHostAddress::Any, port))
	{
		std::cout << "Could not start server" << std::endl;
	}
	else
	{
		std::cout << "Listening to port " << port << "..." << std::endl;
	}
}

// This function is called by QTcpServer when a new connection is available. 
void GameServer::incomingConnection(qintptr socketDescriptor)
{
	// We have a new connection
	std::cout << socketDescriptor << " Connecting..." << std::endl;

	// Every new connection will be run in a newly created thread
	TcpSocketThread *thread = new TcpSocketThread(socketDescriptor,this->main_server, this);

	// connect signal/slot
	// once a thread is not needed, it will be beleted later
	connect(thread, SIGNAL(finished()), thread, SLOT(deleteLater()));

	thread->start();
}