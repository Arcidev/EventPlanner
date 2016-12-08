import React from "react";
import ReactDOM from "react-dom";
import axios from "axios";
import Pusher from "pusher-js";
import {GoogleMapLoader, GoogleMap, Marker} from "react-google-maps";

import 'bootstrap/dist/css/bootstrap.css';
import '../../Styles/site.css';
import './eventEdit.jsx';

const getBaseUrl = function () {
    var url = window.location.protocol + "//" + window.location.host + "/" + window.location.pathname+"/";
    return url;
}

const getCssClass = function (userChoice)
{
    return userChoice === 1 ? "ep-yes" :
           userChoice === 0 ? "ep-no" :
           userChoice === 2 ? "ep-may" :
           "ep-blanc";
}

class UserRow extends React.Component {
    render() {
        var cells = [];

        cells.push(<td>{this.props.row.userName}</td>);
        this.props.row.choices.forEach(function(userChoice) {
            var choiceHtml =
                userChoice === 1 ? <span className="glyphicon glyphicon-ok-sign"></span> :
            userChoice === 0 ? <span className="glyphicon glyphicon-remove-sign"></span> :
            <span className="glyphicon glyphicon-question-sign"></span>;

            cells.push(<td className={getCssClass(userChoice)}>{choiceHtml}</td>);
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
    constructor(props) {
        super(props);

        this.state = this.getBlancForm();
    }

    getBlancForm()
    {
        var arr = new Array(this.props.hourCount).fill(-1);
        return {
            editRow: {
                userName: "",
                hours: arr 
            }
        };
    }

    resetForm(){
        this.setState(this.getBlancForm());
    }

    setHourState(index, value)
    {
        var newHours = this.state.editRow.hours.slice();
        newHours[index] = value;
        this.setState({editRow: {userName: this.state.editRow.userName, hours: newHours}});
    }

    handleNameChange(e)
    {
        this.setState({editRow: {userName: e.target.value, hours: this.state.editRow.hours}});
    }

    handleYes(index) {
        this.setHourState(index,1)
    }

    handleMaybe(index){
        this.setHourState(index,2)
    }

    handleNo(index){
        this.setHourState(index,0)
    }

    handleSave(){
        axios
             .post(getBaseUrl()+`save-choices`, this.state.editRow)
             .catch(() => alert('Something went wrong :( '));
    }

    render() {
        if(this.props.hourCount !== this.state.editRow.hours.length)
        {
            this.resetForm();
        }

        var checkboxCells = [];

        var _that = this;

        this.state.editRow.hours.forEach(function(choice, index) {
            
            checkboxCells.push(
                <td className={getCssClass(choice)}>
                    <ul>
                    <li><a href="#" onClick={_that.handleYes.bind(_that, index)}>Yes</a></li>
                    <li><a href="#" onClick={_that.handleMaybe.bind(_that, index)}>Maybe</a></li>
                    <li><a href="#" onClick={_that.handleNo.bind(_that, index)}>No</a></li>
                    </ul>
                </td>);
        });

        return (
            <tr>
            <td>
                <div className="input-group">
                    <span className="input-group-btn">
                        <input type="button" value="Save" className="btn btn-success" onClick={this.handleSave.bind(this)} />
                    </span>
                    <input onChange={this.handleNameChange.bind(this)} type="text" value={this.state.editRow.userName} className="form-control ep-width150" />
                </div>
            </td>
                {checkboxCells}
            </tr>
        );
    }
}

class EventTable extends React.Component {

    getTableHourCount() {
        var possibleChoices = [];
        this.props.table.header.dates.forEach(function (date) {
            date.hours.forEach(function () {
                possibleChoices.push(-1);
            });
        });

        return possibleChoices.length;
    }

    render() {
        var userRows = [];
        this.props.table.userRows.forEach(function (row)
        {
            userRows.push(<UserRow row={row} />);
        });

        return (
        <table className="table table-striped ep-sheet">
            <TableHeader header={this.props.table.header} />
            <tbody>
                {userRows}
                <UserEditRow hourCount={this.getTableHourCount()} />
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
    constructor()
    {
        super();
        this.state ={
            selectedPlaceId: 1,
            markers: [],
            tables: []
        }
    }

    componentDidMount() {
        //makes post to /event/{eventId}/get handled by webapi therefore making use of event Id already present in the url
        axios
            .get(getBaseUrl() + `get`)
            .then(response => {
                this.setState(response.data);
            })
            .catch((e) => 
            {
                alert('Failed loading table. :( ');
                console.error(e);
            });
        this.setState({});
    }

    onMarkerRightclick(index) {
        var marker = this.state.markers[index];
        this.setState({selectedPlaceId: marker.key});
    }

    getSelectedMarker()
    {
        var candidates = this.state.markers.filter(m => m.key === this.state.selectedPlaceId);
        if(candidates.length == 0){
            console.log("No marker was found by eventId: "+this.state.selectedPlaceId)
            return {title: ""};
        }
        return candidates[0];
    }

    getSelectedTable()
    {
        var candidates = this.state.tables.filter(m => m.key === this.state.selectedPlaceId);
        if(candidates.length == 0){
            console.log("No table was found by eventId: "+this.state.selectedPlaceId)
            return  this.getEmptyTable();
        }
        return candidates[0];
    }

    getEmptyTable() {
        return {
            header: {dates: []},
            userEditRow: { userName: "", hours: []},
            userRows: []
        }
    }

    render() {
        var center = { lat: 59.938043, lng: 30.337157 };
        var zoom = 9;
        return (
            <div className="panel panel-primary">
                <div className="thumbnail ep-map">
                    <GoogleMapLoader containerElement={
                        <div
                          style={{
                        height: "100%",
                    }}
                        />
}
    googleMapElement={
      <GoogleMap
    defaultZoom={3}
    defaultCenter={center}>
    {this.state.markers.map((marker, index) => {
        return (<Marker onClick={this.onMarkerRightclick.bind(this, index)} {...marker} />);
    })}
                        </GoogleMap>
                        }
                    />
                </div>
                <div className="panel-heading"><h4>{this.getSelectedMarker().title}</h4></div>
                <EventTable table={this.getSelectedTable()} editRow={this.state.editRow} />
            </div>
        );
    }
}

const eventDetail = document.getElementById('event-detail');
if (eventDetail) {
    ReactDOM.render(<EventDetailLayout />, eventDetail);
}
