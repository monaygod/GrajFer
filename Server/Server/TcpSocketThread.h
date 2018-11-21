#pragma once

#include <QThread>
#include <QTcpSocket>
#include <QDebug>
#include "ServerGrajFer.h"

class TcpSocketThread : public QThread
{
	Q_OBJECT
private:
	QTcpSocket *socket;
	qintptr socketDescriptor;
	ServerGrajFer *main_server;

public:
	explicit TcpSocketThread(qintptr ID, ServerGrajFer *_server, QObject *parent = 0);

	void run();

signals:
	void error(QTcpSocket::SocketError socketerror);

public slots:
	void readyRead();
	void disconnected();

};
