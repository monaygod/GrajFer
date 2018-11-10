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
#include <QtWidgets/QHBoxLayout>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenu>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QProgressBar>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QToolBar>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_FrontendClass
{
public:
    QWidget *centralWidget;
    QWidget *layoutWidget;
    QHBoxLayout *horizontalLayout;
    QPushButton *Minus;
    QProgressBar *progressBar;
    QPushButton *Plus;
    QPushButton *pushButton;
    QMenuBar *menuBar;
    QMenu *menu_File;
    QToolBar *mainToolBar;
    QStatusBar *statusBar;

    void setupUi(QMainWindow *FrontendClass)
    {
        if (FrontendClass->objectName().isEmpty())
            FrontendClass->setObjectName(QStringLiteral("FrontendClass"));
        FrontendClass->resize(605, 230);
        centralWidget = new QWidget(FrontendClass);
        centralWidget->setObjectName(QStringLiteral("centralWidget"));
        layoutWidget = new QWidget(centralWidget);
        layoutWidget->setObjectName(QStringLiteral("layoutWidget"));
        layoutWidget->setGeometry(QRect(30, 30, 551, 51));
        horizontalLayout = new QHBoxLayout(layoutWidget);
        horizontalLayout->setSpacing(6);
        horizontalLayout->setContentsMargins(11, 11, 11, 11);
        horizontalLayout->setObjectName(QStringLiteral("horizontalLayout"));
        horizontalLayout->setContentsMargins(0, 0, 0, 0);
        Minus = new QPushButton(layoutWidget);
        Minus->setObjectName(QStringLiteral("Minus"));

        horizontalLayout->addWidget(Minus);

        progressBar = new QProgressBar(layoutWidget);
        progressBar->setObjectName(QStringLiteral("progressBar"));
        progressBar->setValue(24);

        horizontalLayout->addWidget(progressBar);

        Plus = new QPushButton(layoutWidget);
        Plus->setObjectName(QStringLiteral("Plus"));

        horizontalLayout->addWidget(Plus);

        pushButton = new QPushButton(centralWidget);
        pushButton->setObjectName(QStringLiteral("pushButton"));
        pushButton->setGeometry(QRect(250, 100, 91, 31));
        FrontendClass->setCentralWidget(centralWidget);
        menuBar = new QMenuBar(FrontendClass);
        menuBar->setObjectName(QStringLiteral("menuBar"));
        menuBar->setGeometry(QRect(0, 0, 605, 21));
        menu_File = new QMenu(menuBar);
        menu_File->setObjectName(QStringLiteral("menu_File"));
        FrontendClass->setMenuBar(menuBar);
        mainToolBar = new QToolBar(FrontendClass);
        mainToolBar->setObjectName(QStringLiteral("mainToolBar"));
        FrontendClass->addToolBar(Qt::TopToolBarArea, mainToolBar);
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
        Minus->setText(QApplication::translate("FrontendClass", "Minus", nullptr));
        Plus->setText(QApplication::translate("FrontendClass", "Plus", nullptr));
        pushButton->setText(QApplication::translate("FrontendClass", "PushButton", nullptr));
        menu_File->setTitle(QApplication::translate("FrontendClass", "&File", nullptr));
    } // retranslateUi

};

namespace Ui {
    class FrontendClass: public Ui_FrontendClass {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_FRONTEND_H
