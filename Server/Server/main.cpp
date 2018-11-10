#include <QtCore/QCoreApplication>
#include <QCommandLineParser>

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



	return app.exec();
}
