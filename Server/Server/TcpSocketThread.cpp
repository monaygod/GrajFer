#include "TcpSocketThread.h"
#include <iostream>


TcpSocketThread::TcpSocketThread(qintptr ID, ServerGrajFer *_server, QObject *parent) :
	QThread(parent)
{
	this->main_server = _server;
	this->socketDescriptor = ID;
}

void TcpSocketThread::run()
{
	// thread starts here
	std::cout << " Thread started" << std::endl;

	socket = new QTcpSocket();

	// set the ID
	if (!socket->setSocketDescriptor(this->socketDescriptor))
	{
		// something's wrong, we just emit a signal
		emit error(socket->error());
		return;
	}

	// connect socket and signal
	// note - Qt::DirectConnection is used because it's multithreaded
	//        This makes the slot to be invoked immediately, when the signal is emitted.

	connect(socket, SIGNAL(readyRead()), this, SLOT(readyRead()), Qt::DirectConnection);
	connect(socket, SIGNAL(disconnected()), this, SLOT(disconnected()));

	// We'll have multiple clients, we want to know which is which
	std::cout << socketDescriptor << " Client connected" << std::endl;

	// make this thread a loop,
	// thread will stay alive so that signal/slot to function properly
	// not dropped out in the middle when thread dies

	exec();
}

void TcpSocketThread::readyRead()
{
	// get the information
	QByteArray Data = socket->readAll();

	// will write on server side window
	std::cout << socketDescriptor << " Data in: " << Data.toStdString() << std::endl;

	socket->write(Data);
}

void TcpSocketThread::disconnected()
{
	std::cout << socketDescriptor << " Disconnected" << std::endl;


	socket->deleteLater();
	exit(0);
}
