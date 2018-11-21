#include <QtCore/QCoreApplication>
#include <QCommandLineParser>
#include "ServerGrajFer.h"

int main(int argc, char *argv[])
{
	QCoreApplication app(argc, argv);
	app.setOrganizationName("Grupa");
	app.setApplicationName("GrajFer");

	//Dodawanie opcji
	QCommandLineParser parser;
	parser.addHelpOption();
	parser.addVersionOption();
	//Tutaj mozna dodac funkcje jakie ma przyjmowac parser

	parser.process(app);

	ServerGrajFer server;
	server.initServer();

	return app.exec();
}
