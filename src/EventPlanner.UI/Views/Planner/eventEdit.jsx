import React from "react";
import ReactDOM from "react-dom";
import axios from "axios";
import Pusher from "pusher-js";
import GoogleMap from 'google-map-react';

import 'bootstrap/dist/css/bootstrap.css';
import '../../Styles/site.css';
import {getBaseUrl} from './commonScript.jsx';

var center = { lat: 49.1951, lng: 16.6068 };
var zoom = 3;
var map = null;
var markerIcon = {
    path: google.maps.SymbolPath.CIRCLE,
    scale: 15.0,
    fillColor: "#FFF",
    fillOpacity: 0.9,
    strokeWeight: 0.7,
}
var mapMarkers = [];
var activeMarker = null;

const K_WIDTH = 40;
const K_HEIGHT = 40;

const greatPlaceStyle = {
    // initially any map object has left top corner at lat lng coordinates
    // it's on you to set object origin to 0,0 coordinates
    position: 'absolute',
    width: K_WIDTH,
    height: K_HEIGHT,
    left: -K_WIDTH / 2,
    top: -K_HEIGHT / 2,

    border: '5px solid #f44336',
    borderRadius: K_HEIGHT,
    backgroundColor: 'white',
    textAlign: 'center',
    color: '#3f51b5',
    fontSize: 16,
    fontWeight: 'bold',
    padding: 4
};

var myEvent = [];
var PeopleRowIDs = [];
var DateTimeRowIDs = [];

class BasicInfoBlock extends React.Component {

    constructor(props) {
        super(props);

        this.state = { 
            name : "Event Name",
            desc : "Event Description"
        };
    }

    componentDidMount() {
        axios
        .get(getBaseUrl()+`get`)
        .then((response) => {
            this.setState({
                name: response.data.name,
                desc: response.data.desc
            });
            myEvent = response.data;
            console.log("Response data:");
            console.log(JSON.stringify(myEvent));
        })
        .catch((e) => 
        {
            console.error(e);
        });
    }

    handleSave(){
        //basic
        myEvent.name = this.refs.eventName.value;
        myEvent.desc = this.refs.eventDesc.value;

        //people
        var pplCount = 0;
        PeopleRowIDs.forEach(function(person) {          
            myEvent.people[pplCount] =  document.getElementById(person).value;
            pplCount++;
        });
        //remove empty people
        for(var i = myEvent.people.length - 1; i >= 0; i--) {
            if(myEvent.people[i] === "") {
                myEvent.people.splice(i, 1);
            }
        }

        //dates
        var dtCount = 0;
        DateTimeRowIDs.forEach(function(dateID) {
            var datetimeval = document.getElementById(dateID).value;;
            var utcDate = new Date(datetimeval).toUTCString();
            myEvent.dates[dtCount] =  utcDate;
            dtCount++;
        });

        //markers
        var mrkrCount = 0;
        mapMarkers.forEach(function(mapMarker){
            var marker = {
                "title" : mapMarker.title,
                "key" : mrkrCount,
                "position" : mapMarker.position
            }
            myEvent.markers[mrkrCount] = marker;
            mrkrCount++;
        });

        console.log("Saving data:");
        console.log(JSON.stringify(myEvent));

        axios
             .post(getBaseUrl()+`save`, myEvent)
             .catch(() => alert('Something went wrong :( '));
    }

    render(){
        return(
                <form className="form-horizontal">
                    <div className="form-group">
                        <label htmlFor="eventName" className="col-sm-2 control-label">Name</label>
                        <div className="col-sm-10">
                        <input type="text" id="eventName" ref="eventName" key={this.state.name} className="form-control" placeholder="Event name" defaultValue={this.state.name}/>
                        </div>
                    </div>
                    <div className="form-group">
                        <label htmlFor="eventDesc" className="col-sm-2 control-label">Description</label>
                        <div className="col-sm-10">
                        <textarea id="eventDesc" ref="eventDesc" key={this.state.desc} className="form-control" rows="3" defaultValue={this.state.desc}></textarea>
                        </div>
                    </div>
                    <div className="form-group">
                        <div className="col-sm-offset-2 col-sm-10">
                        <button type="submit" className="btn btn-default" onClick={this.handleSave.bind(this)} >Save event</button>
                        </div>
                    </div>
                </form>
            );
    }
}


class PeopleRows extends React.Component {
    constructor(props) {
        super(props);

        this.state = { 
            people : [],
        };
    }

    componentDidMount() {
        axios
        .get(getBaseUrl()+`get`)
        .then((response) => {
            this.setState({
                people: response.data.people
            });
        })
        .catch((e) => 
        {
            console.error(e);
        });
    }

    handleAdd(){
        this.state.people.push("");
        this.forceUpdate();
    }


    render() {
        PeopleRowIDs = [];
        var rows = [];
        var count = 0;
        this.state.people.forEach(function (row)
        {
            var rowId = "personEmail" + count;
            PeopleRowIDs.push(rowId);

            //fixes warning fb.me/react-warning-keys
            var divId = "personEmailDivID" + count;
            var LabelId = "personEmailLabelID" + count;
            var divColId = "personEmaildivColID" + count;

            rows.push
            (
                    <div key={divId} className="form-group">
                        <label key={LabelId} htmlFor={rowId} className="col-sm-2 control-label">Person's email</label>
                        <div key={divColId} className="col-sm-10">
                            <input type="email" key={rowId} id={rowId} ref={rowId} className="form-control" defaultValue={row} />
                        </div>
                    </div>
            );
            count++;
        });
     
        return (   
            <div>
                <button type="button" className="btn btn-default" onClick={this.handleAdd.bind(this)}>Add people</button>
                <div>
                    {rows}
                </div>
            </div>
        );
    }
}

class PeopleBlock extends React.Component {
    render(){
        return(
            <form className="form-horizontal">             
                <PeopleRows/>
            </form>
        );
    }
}

class DateTimeRows extends React.Component{
    constructor(props) {
        super(props);

        this.state = { 
            dates : [],
            areUsersSigned : false
        };
    }

    componentDidMount() {
        axios
        .get(getBaseUrl()+`get`)
        .then((response) => {
            this.setState({
                dates: response.data.dates,
                areUsersSigned: response.data.areUsersSigned
            });
            myEvent = response.data;
        })
        .catch((e) => 
        {
            console.error(e);
        });

        
    }

    handleAdd(){  

        if(this.state.areUsersSigned)
            return;

        this.state.dates.push("2016-01-01T00:00:00");
        this.forceUpdate();
    }

    render(){

        var areUsersSigned = this.state.areUsersSigned;
        var btnClassName = "btn btn-default";
        if(areUsersSigned){
            btnClassName += " disabled";
        }
        var btn = <button type="button" id="add_date_btn" ref="add_date_btn" className={btnClassName} onClick={this.handleAdd.bind(this)}>Add date</button>;

        DateTimeRowIDs = [];
        var rows = [];
        var count = 0;
        this.state.dates.forEach(function(date){

            var rowId = "eventDate" + count;
            DateTimeRowIDs.push(rowId);

            //fixes warning fb.me/react-warning-keys
            var divId = "eventDateDivID" + count;
            var LabelId = "eventDateLabelID" + count;
            var divColId = "eventDatedivColID" + count;

            if(areUsersSigned)
                var myInputRow = <input type="datetime-local" key={rowId} id={rowId} className="form-control" defaultValue={date} disabled/>;
            else
                var myInputRow = <input type="datetime-local" key={rowId} id={rowId} className="form-control" defaultValue={date}/>;

            rows.push
            (
                <div key={divId} className="form-group">
                    <label key={LabelId} htmlFor={rowId} className="col-sm-2 control-label">Datetime</label>
                    <div key={divColId} className="col-sm-10">
                        {myInputRow}
                    </div>
                </div>
            );
            count++;
        });                 

   
            return(
                <div>
                {btn}
                <div>
                    {rows}
                </div>
            </div>
        );
        }
}

class DateTimeBlock extends React.Component {
    render(){
        return(
            <form className="form-horizontal">
                <DateTimeRows/>
            </form>
        );
    }
}


class GoogleMapBlock extends React.Component{   
       
    constructor(props) {
        super(props);

        this.state = { 
            markers: [],
            areUsersSigned: false
        };
    }

    componentDidMount() {
        axios
        .get(getBaseUrl()+`get`)
        .then((response) => {
            this.setState({
                markers: response.data.markers,
                areUsersSigned: response.data.areUsersSigned
            });
        })
        .catch((e) => 
        {
            console.error(e);
        });

        var myOptions = {
            zoom: zoom,
            center: center,
        }

        map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);  
        
        google.maps.event.addListener(map, 'click', function(event) {
            var marker = new google.maps.Marker({
                position: event.latLng, 
                map: map,
                title: "",
                label: "",
                //icon: markerIcon,
                animation: google.maps.Animation.DROP
            });

            marker.addListener('click', function() {
                map.setZoom(zoom+2);
                map.setCenter(marker.getPosition());
                document.getElementById("eventPlace0").value = marker.title;
                activeMarker = marker;
            });

            mapMarkers.push(marker);
        });
    }

    handleApply(){

        if(this.state.areUsersSigned)
            return;

        var newPlaceName = document.getElementById("eventPlace0").value;
        activeMarker.setTitle(newPlaceName);
        activeMarker.setLabel(newPlaceName);
    }

    render(){
        var areUsersSigned = this.state.areUsersSigned;

        var className = areUsersSigned ? ("btn btn-default disabled") : ("btn btn-default");
        var btnApply = <button type="button" className={className}  onClick={this.handleApply.bind(this)}>Apply</button>;

        var markerCount = 0;
        this.state.markers.forEach(function(marker){
            var marker = new google.maps.Marker({
                position: marker.position,
                map: map,
                title: marker.title,
                label: marker.title,
                //icon: markerIcon
            });

            if(!areUsersSigned){
                marker.addListener('click', function() {
                    map.setZoom(zoom+2);
                    map.setCenter(marker.getPosition());
                    document.getElementById("eventPlace0").value = marker.title;
                    activeMarker = marker;
                });
            }

            mapMarkers.push(marker);

        });
     
        return(
            <div>
            <div id="map_canvas" className="thumbnail ep-map">
            </div>
            <form className="form-horizontal">
                    <div className="form-group">
                        <label htmlFor="eventPlace0" className="col-sm-2 control-label">Place name</label>
                        <div className="col-sm-10">
                            <input type="text" id="eventPlace0" className="form-control" />
                        </div>
                    </div>
                    <div className="form-group">
                        <div className="col-sm-offset-2 col-sm-10">
                            {btnApply}
                        </div>
                    </div>
            </form>  
            </div>
            );
    }
}

class EventEditLayout extends React.Component {
    render() {

        let styles = {
                panel: {
                border: "solid #f1f1f1",
                borderWidth: "1 1px",
                borderRadius: "8px",

                padding: "15px"
                }
        }

        var mapUI = true;

        return (
            <div>

            <h2>Basic information</h2>
                <div style={styles.panel} >
                <BasicInfoBlock/>
                </div>
            <h2>People</h2>
                <div style={styles.panel} >
                <PeopleBlock/>
                </div>
            <h2>Date and Time</h2>
                <div style={styles.panel} >
                <DateTimeBlock/>
                </div>
            <h2>Place</h2>
                <div style={styles.panel} >
                <div>
                <GoogleMapBlock/>
                </div>                                 
                </div>
            </div>
        );
    }
}



const eventEdit = document.getElementById('event-edit');
if (eventEdit) {
    ReactDOM.render(<EventEditLayout />, eventEdit);
}