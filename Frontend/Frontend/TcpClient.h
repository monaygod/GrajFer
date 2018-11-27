#pragma once

#include <QObject>
#include <QTcpSocket>
#include <QDataStream>
#include <QNetworkSession>


class TcpClient : public QObject
{
	Q_OBJECT
private:
	QTcpSocket *tcpSocket = nullptr;
	QDataStream in;
	QNewtorkSession * networkSession = nullptr;



public:
	TcpClient(QObject *parent);
	~TcpClient();
	bool socket_enabled;
};
