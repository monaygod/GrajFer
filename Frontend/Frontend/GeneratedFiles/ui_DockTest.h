/********************************************************************************
** Form generated from reading UI file 'DockTest.ui'
**
** Created by: Qt User Interface Compiler version 5.11.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DOCKTEST_H
#define UI_DOCKTEST_H

#include <QtCore/QVariant>
#include <QtWidgets/QApplication>
#include <QtWidgets/QDockWidget>
#include <QtWidgets/QGridLayout>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QListWidget>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QTreeWidget>
#include <QtWidgets/QVBoxLayout>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_DockTest
{
public:
    QWidget *centralWidget;
    QGridLayout *gridLayout;
    QVBoxLayout *verticalLayout;
    QListWidget *listWidget;
    QTreeWidget *treeWidget;
    QDockWidget *dockWidget;
    QWidget *dockWidgetContents;
    QDockWidget *dockWidget_2;
    QWidget *dockWidgetContents_2;

    void setupUi(QMainWindow *DockTest)
    {
        if (DockTest->objectName().isEmpty())
            DockTest->setObjectName(QStringLiteral("DockTest"));
        DockTest->resize(777, 512);
        centralWidget = new QWidget(DockTest);
        centralWidget->setObjectName(QStringLiteral("centralWidget"));
        gridLayout = new QGridLayout(centralWidget);
        gridLayout->setSpacing(6);
        gridLayout->setContentsMargins(11, 11, 11, 11);
        gridLayout->setObjectName(QStringLiteral("gridLayout"));
        verticalLayout = new QVBoxLayout();
        verticalLayout->setSpacing(6);
        verticalLayout->setObjectName(QStringLiteral("verticalLayout"));
        listWidget = new QListWidget(centralWidget);
        listWidget->setObjectName(QStringLiteral("listWidget"));

        verticalLayout->addWidget(listWidget);

        treeWidget = new QTreeWidget(centralWidget);
        QTreeWidgetItem *__qtreewidgetitem = new QTreeWidgetItem();
        __qtreewidgetitem->setText(0, QStringLiteral("1"));
        treeWidget->setHeaderItem(__qtreewidgetitem);
        treeWidget->setObjectName(QStringLiteral("treeWidget"));

        verticalLayout->addWidget(treeWidget);


        gridLayout->addLayout(verticalLayout, 0, 0, 1, 1);

        DockTest->setCentralWidget(centralWidget);
        dockWidget = new QDockWidget(DockTest);
        dockWidget->setObjectName(QStringLiteral("dockWidget"));
        dockWidgetContents = new QWidget();
        dockWidgetContents->setObjectName(QStringLiteral("dockWidgetContents"));
        dockWidget->setWidget(dockWidgetContents);
        DockTest->addDockWidget(static_cast<Qt::DockWidgetArea>(2), dockWidget);
        dockWidget_2 = new QDockWidget(DockTest);
        dockWidget_2->setObjectName(QStringLiteral("dockWidget_2"));
        dockWidgetContents_2 = new QWidget();
        dockWidgetContents_2->setObjectName(QStringLiteral("dockWidgetContents_2"));
        dockWidget_2->setWidget(dockWidgetContents_2);
        DockTest->addDockWidget(static_cast<Qt::DockWidgetArea>(2), dockWidget_2);

        retranslateUi(DockTest);

        QMetaObject::connectSlotsByName(DockTest);
    } // setupUi

    void retranslateUi(QMainWindow *DockTest)
    {
        DockTest->setWindowTitle(QApplication::translate("DockTest", "DockTest", nullptr));
    } // retranslateUi

};

namespace Ui {
    class DockTest: public Ui_DockTest {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DOCKTEST_H
