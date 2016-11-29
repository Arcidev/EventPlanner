import React from "react";
import ReactDOM from "react-dom";
import axios from "axios";
import Pusher from "pusher-js";
import GoogleMap from 'google-map-react';

import 'bootstrap/dist/css/bootstrap.css';
import '../../Styles/site.css';
import './eventEdit.jsx';

const baseUrl = 'http://localhost:13692/';

class UserRow extends React.Component {
    render() {
        var cells = [];

        cells.push(<td>{this.props.row.userName}</td>);
        this.props.row.choices.forEach(function(userChoice) {
            var choiceHtml =
                userChoice === 1 ? <span className="glyphicon glyphicon-ok-sign"></span> :
            userChoice === 0 ? <span className="glyphicon glyphicon-remove-sign"></span> :
            <span className="glyphicon glyphicon-question-sign"></span>;

            cells.push(<td>{choiceHtml}</td>);
        });

        return (<tr>{cells}</tr>);
    }
}

class TableHeader extends React.Component {
    render() {
        var dateCells = [];
        var hourCells = [];

        this.props.header.dates.forEach(function(date) {
            dateCells.push(<th colSpan={date.hours.length}>{date.value}</th>);
        date.hours.forEach(function(hour){
            hourCells.push(<th>{hour}</th>);
        });
    });

    return (
        <thead>
        <tr><th></th>{dateCells}</tr>
        <tr><th></th>{hourCells}</tr>
        </thead>
    );
    }
}

class UserEditRow extends React.Component {
    render() {
        var checkboxCells = [];
        this.props.header.dates.forEach(function(date) {
            date.hours.forEach(function(hour){
                checkboxCells.push(<td><input type="checkbox" /></td>);
            });
        });
        
        return (
            <tr>
            <td><input type="text" className="form-control ep-width200" /></td>
            {checkboxCells}
            </tr>
        );
    }
}

class EventTable extends React.Component {
    render() {
        var userRows = [];
        this.props.table.userRows.forEach(function (row)
        {
            userRows.push(<UserRow row={row} />);
        });
        
        return (
            <table className="table table-striped">
                <TableHeader header={this.props.table.header}/>
                <tbody>
                    {userRows}
                    <UserEditRow header={this.props.table.header} />
                </tbody>
            </table>
        );
    }
}

class GoogleMarker extends React.Component {
    render() {
        return (<div className="ep-marker">{this.props.placeName}</div>);
    }
}


class EventDetailLayout extends React.Component {
    render() {
        var table = {
            userRows: [
                {
                    userName: "Nick",
                    choices: [1,0,2,1,2]
                },
                {
                    userName: "Judy",
                    choices: [1,1,1,2,2]
                }
            ],
            header: {
                dates: [
                    {
                        value: "2. 3. 2016",
                        hours: ["8:00","9:00", "10:00"]
                    },
                    {
                        value: "2. 4. 2019",
                        hours: ["10:00", "11:00"]
                    }
                ]
            }
        }


        var center = { lat: 59.938043, lng: 30.337157 };
        var zoom = 9;
        return (
            <div className="panel panel-primary">
                <div className="thumbnail ep-map">
                    <GoogleMap defaultCenter={center} defaultZoom={zoom}>
                        <GoogleMarker placeName="place A"/>
                    </GoogleMap>
                </div>
                <div className="panel-heading"><h4>Name of Selected Place</h4></div>
                <EventTable table={table} />
            </div>
        );
    }
}

class EventDashboardLayout extends React.Component {
    render() {
        return (
            <div className="row">
                <div className="panel panel-primary">
                    <div className="panel-heading">My events - Created</div>
                    <div className="panel-body">
                        <div className="row ep-event">
                            <h4 className="col-md-3">Event Name</h4>
                            <div className="col-md-9">
                                <div className="btn-group pull-right">
                                    <button className="btn btn-default navbar-btn nav-pills"><span className="glyphicon glyphicon-edit"></span>Edit event</button>
                                    <button className="btn btn-default navbar-btn nav-pills"><span className="glyphicon glyphicon-link"></span>Copy link</button>
                                    <button className="btn btn-default navbar-btn nav-pills"><span className="glyphicon glyphicon-copy"></span>Fill in</button>
                                </div>
                            </div>
                         </div>
                    </div>
                </div>
            </div>
            );
    }
}

const eventDetail = document.getElementById('event-detail');
if (eventDetail) {
    ReactDOM.render(<EventDetailLayout />, eventDetail);
}
const dashboard = document.getElementById('dashboard');
if (dashboard) {
    ReactDOM.render(<EventDashboardLayout />, dashboard);
}
