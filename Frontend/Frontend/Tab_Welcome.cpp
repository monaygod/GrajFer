#include "stdafx.h"
#include "Tab_Welcome.h"

Tab_Welcome::Tab_Welcome(QWidget *parent)
	: QWidget(parent)
{
	ui.setupUi(this);
	QFont *newFont;

	newFont = new QFont("Arial", 50);
	ui.Welcome->setText("Welcome");
	ui.Welcome->setFont(*newFont);
	ui.Welcome->setAlignment(Qt::AlignCenter);
	delete newFont;

	newFont = new QFont("Arial", 10);
	ui.Description->setText("A simple gameboard creator, make your own dream game.");
	ui.Description->setFont(*newFont);
	ui.Description->setAlignment(Qt::AlignCenter);
	delete newFont;

	newFont = new QFont("Arial", 7);
	ui.Authors->setText("Authors:\nJonatan Dalasinski\nDominik Szymanek\nRobert Grudek\nJakub Czerwionka");
	ui.Authors->setFont(*newFont);
	ui.Authors->setAlignment(Qt::AlignCenter);
	delete newFont;
}

Tab_Welcome::~Tab_Welcome()
{
}
