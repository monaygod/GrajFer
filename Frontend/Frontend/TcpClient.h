#pragma once

#include <QObject>

class TcpClient : public QObject
{
	Q_OBJECT

public:
	TcpClient(QObject *parent);
	~TcpClient();
};
