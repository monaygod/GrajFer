/********************************************************************************
** Form generated from reading UI file 'Tab_Creator.ui'
**
** Created by: Qt User Interface Compiler version 5.11.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_TAB_CREATOR_H
#define UI_TAB_CREATOR_H

#include <QtCore/QVariant>
#include <QtWidgets/QApplication>
#include <QtWidgets/QDockWidget>
#include <QtWidgets/QFrame>
#include <QtWidgets/QGridLayout>
#include <QtWidgets/QSplitter>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_Tab_Creator
{
public:
    QGridLayout *gridLayout;
    QSplitter *splitter_2;
    QFrame *frame;
    QSplitter *splitter;
    QDockWidget *dockWidget;
    QWidget *dockWidgetContents_3;
    QDockWidget *dockWidget_2;
    QWidget *dockWidgetContents_4;

    void setupUi(QWidget *Tab_Creator)
    {
        if (Tab_Creator->objectName().isEmpty())
            Tab_Creator->setObjectName(QStringLiteral("Tab_Creator"));
        Tab_Creator->resize(1110, 581);
        gridLayout = new QGridLayout(Tab_Creator);
        gridLayout->setObjectName(QStringLiteral("gridLayout"));
        splitter_2 = new QSplitter(Tab_Creator);
        splitter_2->setObjectName(QStringLiteral("splitter_2"));
        splitter_2->setOrientation(Qt::Horizontal);
        frame = new QFrame(splitter_2);
        frame->setObjectName(QStringLiteral("frame"));
        frame->setFrameShape(QFrame::StyledPanel);
        frame->setFrameShadow(QFrame::Raised);
        splitter_2->addWidget(frame);
        splitter = new QSplitter(splitter_2);
        splitter->setObjectName(QStringLiteral("splitter"));
        splitter->setOrientation(Qt::Vertical);
        dockWidget = new QDockWidget(splitter);
        dockWidget->setObjectName(QStringLiteral("dockWidget"));
        dockWidgetContents_3 = new QWidget();
        dockWidgetContents_3->setObjectName(QStringLiteral("dockWidgetContents_3"));
        dockWidget->setWidget(dockWidgetContents_3);
        splitter->addWidget(dockWidget);
        dockWidget_2 = new QDockWidget(splitter);
        dockWidget_2->setObjectName(QStringLiteral("dockWidget_2"));
        dockWidgetContents_4 = new QWidget();
        dockWidgetContents_4->setObjectName(QStringLiteral("dockWidgetContents_4"));
        dockWidget_2->setWidget(dockWidgetContents_4);
        splitter->addWidget(dockWidget_2);
        splitter_2->addWidget(splitter);

        gridLayout->addWidget(splitter_2, 0, 0, 1, 1);


        retranslateUi(Tab_Creator);

        QMetaObject::connectSlotsByName(Tab_Creator);
    } // setupUi

    void retranslateUi(QWidget *Tab_Creator)
    {
        Tab_Creator->setWindowTitle(QApplication::translate("Tab_Creator", "Form", nullptr));
    } // retranslateUi

};

namespace Ui {
    class Tab_Creator: public Ui_Tab_Creator {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_TAB_CREATOR_H
