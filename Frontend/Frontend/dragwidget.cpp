#include "stdafx.h"
#include <QtWidgets>

#include "dragwidget.h"

DragWidget::DragWidget(QWidget *parent)
	: QFrame(parent)
{
	setMinimumSize(200, 200);
	setFrameStyle(QFrame::Sunken | QFrame::StyledPanel);
	setAcceptDrops(true);	

	QLabel *boatIcon = new QLabel(this);
	boatIcon->setPixmap(QPixmap(":/boat.png"));
	boatIcon->move(10, 10);
	boatIcon->show();
	boatIcon->setAttribute(Qt::WA_DeleteOnClose);

	QSizeGrip * sizeGrip = new QSizeGrip(boatIcon);

	QGridLayout * layout = new QGridLayout(boatIcon);
	layout->addWidget(sizeGrip, 0, 0, 1, 1, Qt::AlignBottom | Qt::AlignRight);


	QLabel *carIcon = new QLabel(this);
	carIcon->setPixmap(QPixmap(":/car.png"));
	carIcon->move(100, 10);
	carIcon->show();
	carIcon->setAttribute(Qt::WA_DeleteOnClose);

	QLabel *houseIcon = new QLabel(this);
	houseIcon->setPixmap(QPixmap(":/house.png"));
	houseIcon->move(10, 80);
	houseIcon->show();
	houseIcon->setAttribute(Qt::WA_DeleteOnClose);
}

void DragWidget::dragEnterEvent(QDragEnterEvent *event)
{
	if (event->mimeData()->hasFormat("application/x-dnditemdata")) {
		if (event->source() == this) {
			event->setDropAction(Qt::MoveAction);
			event->accept();
		}
		else {
			event->acceptProposedAction();
		}
	}
	else {
		event->ignore();
	}
}

void DragWidget::dragMoveEvent(QDragMoveEvent *event)
{
	if (event->mimeData()->hasFormat("application/x-dnditemdata")) {
		if (event->source() == this) {
			event->setDropAction(Qt::MoveAction);
			event->accept();
		}
		else {
			event->acceptProposedAction();
		}
	}
	else {
		event->ignore();
	}
}

void DragWidget::dropEvent(QDropEvent *event)
{
	if (event->mimeData()->hasFormat("application/x-dnditemdata")) {
		QByteArray itemData = event->mimeData()->data("application/x-dnditemdata");
		QDataStream dataStream(&itemData, QIODevice::ReadOnly);

		QPixmap pixmap;
		QPoint offset;
		dataStream >> pixmap >> offset;

		QLabel *newIcon = new QLabel(this);
		newIcon->setPixmap(pixmap);
		newIcon->move(event->pos() - offset);
		newIcon->setWindowFlag(Qt::SubWindow);

		QSizeGrip * sizeGrip = new QSizeGrip(newIcon);

		QGridLayout * layout = new QGridLayout(newIcon);
		layout->addWidget(sizeGrip, 0, 0, 1, 1, Qt::AlignBottom | Qt::AlignRight);
		newIcon->show();
		newIcon->setAttribute(Qt::WA_DeleteOnClose);

		if (event->source() == this) {
			event->setDropAction(Qt::MoveAction);
			event->accept();
		}
		else {
			event->acceptProposedAction();
		}
	}
	else {
		event->ignore();
	}
}

void DragWidget::mousePressEvent(QMouseEvent *event)
{
	QLabel *child = static_cast<QLabel*>(childAt(event->pos()));
	if (!child)
		return;

	QPixmap pixmap = *child->pixmap();

	QByteArray itemData;
	QDataStream dataStream(&itemData, QIODevice::WriteOnly);
	dataStream << pixmap << QPoint(event->pos() - child->pos());

	QMimeData *mimeData = new QMimeData;
	mimeData->setData("application/x-dnditemdata", itemData);

	QDrag *drag = new QDrag(this);
	drag->setMimeData(mimeData);
	drag->setPixmap(pixmap);
	drag->setHotSpot(event->pos() - child->pos());

	QPixmap tempPixmap = pixmap;
	QPainter painter;
	painter.begin(&tempPixmap);
	painter.fillRect(pixmap.rect(), QColor(127, 127, 127, 127));
	painter.end();

	child->setPixmap(tempPixmap);

	if (drag->exec(Qt::CopyAction | Qt::MoveAction, Qt::CopyAction) == Qt::MoveAction) {
		child->close();
	}
	else {
		child->show();
		child->setPixmap(pixmap);
	}
}