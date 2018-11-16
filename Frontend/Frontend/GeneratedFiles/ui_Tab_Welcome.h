/********************************************************************************
** Form generated from reading UI file 'Tab_Welcome.ui'
**
** Created by: Qt User Interface Compiler version 5.11.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_TAB_WELCOME_H
#define UI_TAB_WELCOME_H

#include <QtCore/QVariant>
#include <QtWidgets/QApplication>
#include <QtWidgets/QLabel>
#include <QtWidgets/QSpacerItem>
#include <QtWidgets/QVBoxLayout>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_Tab_Welcome
{
public:
    QVBoxLayout *verticalLayout;
    QSpacerItem *verticalSpacer;
    QLabel *Welcome;
    QLabel *Description;
    QLabel *Authors;
    QSpacerItem *verticalSpacer_2;

    void setupUi(QWidget *Tab_Welcome)
    {
        if (Tab_Welcome->objectName().isEmpty())
            Tab_Welcome->setObjectName(QStringLiteral("Tab_Welcome"));
        Tab_Welcome->setWindowModality(Qt::NonModal);
        Tab_Welcome->resize(710, 431);
        Tab_Welcome->setContextMenuPolicy(Qt::NoContextMenu);
        verticalLayout = new QVBoxLayout(Tab_Welcome);
        verticalLayout->setSpacing(6);
        verticalLayout->setContentsMargins(11, 11, 11, 11);
        verticalLayout->setObjectName(QStringLiteral("verticalLayout"));
        verticalSpacer = new QSpacerItem(20, 40, QSizePolicy::Minimum, QSizePolicy::Expanding);

        verticalLayout->addItem(verticalSpacer);

        Welcome = new QLabel(Tab_Welcome);
        Welcome->setObjectName(QStringLiteral("Welcome"));

        verticalLayout->addWidget(Welcome);

        Description = new QLabel(Tab_Welcome);
        Description->setObjectName(QStringLiteral("Description"));

        verticalLayout->addWidget(Description);

        Authors = new QLabel(Tab_Welcome);
        Authors->setObjectName(QStringLiteral("Authors"));

        verticalLayout->addWidget(Authors);

        verticalSpacer_2 = new QSpacerItem(20, 40, QSizePolicy::Minimum, QSizePolicy::Expanding);

        verticalLayout->addItem(verticalSpacer_2);


        retranslateUi(Tab_Welcome);

        QMetaObject::connectSlotsByName(Tab_Welcome);
    } // setupUi

    void retranslateUi(QWidget *Tab_Welcome)
    {
        Tab_Welcome->setWindowTitle(QApplication::translate("Tab_Welcome", "Tab_Welcome", nullptr));
        Welcome->setText(QApplication::translate("Tab_Welcome", "TextLabel", nullptr));
        Description->setText(QApplication::translate("Tab_Welcome", "TextLabel", nullptr));
        Authors->setText(QApplication::translate("Tab_Welcome", "TextLabel", nullptr));
    } // retranslateUi

};

namespace Ui {
    class Tab_Welcome: public Ui_Tab_Welcome {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_TAB_WELCOME_H
