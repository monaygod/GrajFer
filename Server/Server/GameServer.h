#pragma once

#include <QTcpServer>
#include "TcpSocketThread.h"
#include "ServerGrajFer.h"

class GameServer : public QTcpServer
{
	Q_OBJECT
private:
	ServerGrajFer *main_server;

protected:
	void incomingConnection(qintptr socketDescriptor);

public:
	explicit GameServer(QObject *parent = 0);
	void startServer(ServerGrajFer *_server);
signals:

public slots:
	
};
