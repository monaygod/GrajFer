/********************************************************************************
** Form generated from reading UI file 'Frontend.ui'
**
** Created by: Qt User Interface Compiler version 5.11.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_FRONTEND_H
#define UI_FRONTEND_H

#include <QtCore/QVariant>
#include <QtWidgets/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QGridLayout>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenu>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QTabWidget>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_FrontendClass
{
public:
    QWidget *centralWidget;
    QGridLayout *gridLayout;
    QTabWidget *tabWidget;
    QMenuBar *menuBar;
    QMenu *menu_File;
    QStatusBar *statusBar;

    void setupUi(QMainWindow *FrontendClass)
    {
        if (FrontendClass->objectName().isEmpty())
            FrontendClass->setObjectName(QStringLiteral("FrontendClass"));
        FrontendClass->resize(1006, 620);
        centralWidget = new QWidget(FrontendClass);
        centralWidget->setObjectName(QStringLiteral("centralWidget"));
        gridLayout = new QGridLayout(centralWidget);
        gridLayout->setSpacing(6);
        gridLayout->setContentsMargins(11, 11, 11, 11);
        gridLayout->setObjectName(QStringLiteral("gridLayout"));
        gridLayout->setContentsMargins(0, 0, 0, 0);
        tabWidget = new QTabWidget(centralWidget);
        tabWidget->setObjectName(QStringLiteral("tabWidget"));

        gridLayout->addWidget(tabWidget, 0, 0, 1, 1);

        FrontendClass->setCentralWidget(centralWidget);
        menuBar = new QMenuBar(FrontendClass);
        menuBar->setObjectName(QStringLiteral("menuBar"));
        menuBar->setGeometry(QRect(0, 0, 1006, 21));
        menu_File = new QMenu(menuBar);
        menu_File->setObjectName(QStringLiteral("menu_File"));
        FrontendClass->setMenuBar(menuBar);
        statusBar = new QStatusBar(FrontendClass);
        statusBar->setObjectName(QStringLiteral("statusBar"));
        FrontendClass->setStatusBar(statusBar);

        menuBar->addAction(menu_File->menuAction());

        retranslateUi(FrontendClass);

        QMetaObject::connectSlotsByName(FrontendClass);
    } // setupUi

    void retranslateUi(QMainWindow *FrontendClass)
    {
        FrontendClass->setWindowTitle(QApplication::translate("FrontendClass", "Frontend", nullptr));
        menu_File->setTitle(QApplication::translate("FrontendClass", "&File", nullptr));
    } // retranslateUi

};

namespace Ui {
    class FrontendClass: public Ui_FrontendClass {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_FRONTEND_H
