#include "stdafx.h"
#include "Frontend.h"
#include <QtWidgets/QApplication>
#include "dragwidget.h"


int main(int argc, char *argv[])
{
	QApplication a(argc, argv);

	QWidget mainWidget;
	/*QHBoxLayout *horizontalLayout = new QHBoxLayout(&mainWidget);
	horizontalLayout->addWidget(new DragWidget);

	mainWidget.setWindowTitle(QObject::tr("Draggable Icons"));
	mainWidget.show();*/

	Frontend w;
	w.show();
	return a.exec();
}
