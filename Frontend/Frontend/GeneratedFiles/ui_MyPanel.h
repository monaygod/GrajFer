/********************************************************************************
** Form generated from reading UI file 'MyPanel.ui'
**
** Created by: Qt User Interface Compiler version 5.11.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_MYPANEL_H
#define UI_MYPANEL_H

#include <QtCore/QVariant>
#include <QtWidgets/QApplication>
#include <QtWidgets/QGridLayout>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_MyPanel
{
public:
    QGridLayout *gridLayout;

    void setupUi(QWidget *MyPanel)
    {
        if (MyPanel->objectName().isEmpty())
            MyPanel->setObjectName(QStringLiteral("MyPanel"));
        MyPanel->resize(640, 298);
        MyPanel->setStyleSheet(QStringLiteral("background-color: rgb(0, 255, 0);"));
        gridLayout = new QGridLayout(MyPanel);
        gridLayout->setSpacing(6);
        gridLayout->setContentsMargins(11, 11, 11, 11);
        gridLayout->setObjectName(QStringLiteral("gridLayout"));
        gridLayout->setContentsMargins(0, 0, 0, 0);

        retranslateUi(MyPanel);

        QMetaObject::connectSlotsByName(MyPanel);
    } // setupUi

    void retranslateUi(QWidget *MyPanel)
    {
        MyPanel->setWindowTitle(QApplication::translate("MyPanel", "MyPanel", nullptr));
    } // retranslateUi

};

namespace Ui {
    class MyPanel: public Ui_MyPanel {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_MYPANEL_H
