#pragma once

#include <QObject>

class CommandHandler : public QObject
{
	Q_OBJECT

public:
	CommandHandler(QObject *parent);
	~CommandHandler();
};
