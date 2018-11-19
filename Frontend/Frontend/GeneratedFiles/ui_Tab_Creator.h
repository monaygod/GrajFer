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
#include <QtWidgets/QFrame>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_Tab_Creator
{
public:
    QFrame *frame;
    QFrame *frame_2;

    void setupUi(QWidget *Tab_Creator)
    {
        if (Tab_Creator->objectName().isEmpty())
            Tab_Creator->setObjectName(QStringLiteral("Tab_Creator"));
        Tab_Creator->resize(1110, 581);
        Tab_Creator->setAutoFillBackground(false);
        Tab_Creator->setStyleSheet(QLatin1String("color: rgb(255, 10, 10);\n"
"background-color: rgb(0, 85, 0);"));
        frame = new QFrame(Tab_Creator);
        frame->setObjectName(QStringLiteral("frame"));
        frame->setGeometry(QRect(10, 10, 871, 561));
        frame->setStyleSheet(QStringLiteral("background-color: rgb(255, 255, 127);"));
        frame->setFrameShape(QFrame::StyledPanel);
        frame->setFrameShadow(QFrame::Raised);
        frame_2 = new QFrame(Tab_Creator);
        frame_2->setObjectName(QStringLiteral("frame_2"));
        frame_2->setGeometry(QRect(890, 10, 211, 561));
        frame_2->setStyleSheet(QStringLiteral("background-color: rgb(110, 6, 255);"));
        frame_2->setFrameShape(QFrame::StyledPanel);
        frame_2->setFrameShadow(QFrame::Raised);

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
