#ifndef RESIZABLEFRAME_H
#define RESIZABLEFRAME_H

#include <QFrame>
#include <QMouseEvent>

class ResizableFrame : public QFrame
{
	Q_OBJECT

public:
	explicit ResizableFrame(QWidget *parent = 0);
	~ResizableFrame();

	enum startPositions { topleft, left, bottomleft, bottom, bottomright, right, topright, top, move } startPositions;
protected:
	void mousePressEvent(QMouseEvent *event);
	void mouseReleaseEvent(QMouseEvent * event);
	void mouseMoveEvent(QMouseEvent *event);

private:
	QPoint dragStartPosition;
	QRect dragStartGeometry;
	enum startPositions startPos;
};

#endif // RESIZABLEFRAME_H