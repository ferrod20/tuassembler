  (function($) {
		var d = new Date();
		d.setDate(d.getDate() - d.getDay());
		var year = d.getFullYear();
		var month = d.getMonth();
		var day = d.getDate();
	
		var eventData1 = {
			options: {
				timeslotsPerHour: 4,
				timeslotHeight: 20,
				defaultFreeBusy: {free: true}
			},
			events : [
				{"id":1, "start": new Date(year, month, day+0, 12), "end": new Date(year, month, day+0, 13, 30), "title": "Lunch with Mike", userId: 0},
				{"id":2, "start": new Date(year, month, day+0, 14), "end": new Date(year, month, day+0, 14, 45), "title": "Dev Meeting", userId: 1},
				{"id":3, "start": new Date(year, month, day+1, 18), "end": new Date(year, month, day+1, 18, 45), "title": "Hair cut", userId: 1},
				{"id":4, "start": new Date(year, month, day+2, 08), "end": new Date(year, month, day+2, 09, 30), "title": "Team breakfast", userId: 0},
				{"id":5, "start": new Date(year, month, day+1, 14), "end": new Date(year, month, day+1, 15, 00), "title": "Product showcase", userId: 1}
			],
			freebusys: [
				{"start": new Date(year, month, day+0, 00), "end": new Date(year, month, day+3, 00, 00), "free": false, userId: [0,1,2,3]},
				{"start": new Date(year, month, day+0, 08), "end": new Date(year, month, day+0, 12, 00), "free": true, userId: [0,1,2,3]},
				{"start": new Date(year, month, day+1, 08), "end": new Date(year, month, day+1, 12, 00), "free": true, userId: [0,1,2,3]},
				{"start": new Date(year, month, day+2, 08), "end": new Date(year, month, day+2, 12, 00), "free": true, userId: [0,1,2,3]},
				{"start": new Date(year, month, day+1, 14), "end": new Date(year, month, day+1, 18, 00), "free": true, userId: [0,1,2,3]},
				{"start": new Date(year, month, day+2, 08), "end": new Date(year, month, day+2, 12, 00), "free": true, userId: [0,3]},
				{"start": new Date(year, month, day+2, 14), "end": new Date(year, month, day+2, 18, 00), "free": true, userId: 1}
			]
		};
	
		d = new Date();
		d.setDate(d.getDate() -(d.getDay() - 3));
		year = d.getFullYear();
		month = d.getMonth();
		day = d.getDate();
	
		var eventData2 = {
			options: {
				timeslotsPerHour: 3,
				timeslotHeight: 30,
				defaultFreeBusy: {free: false}
			},
			events : [
				{"id":1, "start": new Date(year, month, day+0, 12), "end": new Date(year, month, day+0, 13, 00), "title": "Lunch with Sarah", userId: 1},
				{"id":2, "start": new Date(year, month, day+0, 14), "end": new Date(year, month, day+0, 14, 40), "title": "Team Meeting", userId: 0},
				{"id":3, "start": new Date(year, month, day+1, 18), "end": new Date(year, month, day+1, 18, 40), "title": "Meet with Joe", userId: 1},
				{"id":4, "start": new Date(year, month, day-1, 08), "end": new Date(year, month, day-1, 09, 20), "title": "Coffee with Alison", userId: 1},
				{"id":5, "start": new Date(year, month, day+1, 14), "end": new Date(year, month, day+1, 15, 00), "title": "Product showcase", userId: 0}
			],
			freebusys: [
				{"start": new Date(year, month, day-1, 08), "end": new Date(year, month, day-1, 18, 00), "free": true, userId: [0,1,2,3]},
				{"start": new Date(year, month, day+0, 08), "end": new Date(year, month, day+0, 18, 00), "free": true, userId: [0,1,2,3]},
				{"start": new Date(year, month, day+1, 08), "end": new Date(year, month, day+1, 18, 00), "free": true, userId: [0,3]},
				{"start": new Date(year, month, day+2, 14), "end": new Date(year, month, day+2, 18, 00), "free": true, userId: 1}
			]
		};
	
		function updateMessage() {
			var dataSource = $('#data_source').val();
			$('#message').fadeOut(function() {
				if(dataSource === "1") {
					$('#message').text("Displaying event data set 1 with timeslots per hour of 4 and timeslot height of 20px. Moreover, the calendar is free by default.");
				} else if(dataSource === "2") {
					$('#message').text("Displaying event data set 2 with timeslots per hour of 3 and timeslot height of 30px. Moreover, the calendar is busy by default.");
				} else {
					$('#message').text("Displaying no events.");
				}
				$(this).fadeIn();
			});
		}
		
		$(document).ready(function() {
	
	 	$('#switcher').themeswitcher();
	 
			var $calendar = $('#calendar').weekCalendar({
				timeslotsPerHour: 4,
				scrollToHourMillis : 0,
				height: function($calendar){
					return $(window).height() - $('h1').outerHeight(true);
				},
				eventRender : function(calEvent, $event) {
					if(calEvent.end.getTime() < new Date().getTime()) {
						$event.css("backgroundColor", "#aaa");
						$event.find(".wc-time").css({backgroundColor: "#999", border:"1px solid #888"});
					}
				},
				eventNew : function(calEvent, $event) {
         var $dialogContent = $("#event_edit_container");
         resetForm($dialogContent);
         var startField = $dialogContent.find("select[name='start']").val(calEvent.start);
         var endField = $dialogContent.find("select[name='end']").val(calEvent.end);
         var titleField = $dialogContent.find("input[name='title']");
         var bodyField = $dialogContent.find("textarea[name='body']");


         $dialogContent.dialog({
            modal: true,
            title: "Nuevo turno",
            close: function() {
               $dialogContent.dialog("destroy");
               $dialogContent.hide();
               $('#calendar').weekCalendar("removeUnsavedEvents");
            },
            buttons: {
               save : function() {
                  calEvent.id = id;
                  id++;
                  calEvent.start = new Date(startField.val());
                  calEvent.end = new Date(endField.val());
                  calEvent.title = titleField.val();
                  calEvent.body = bodyField.val();

                  $calendar.weekCalendar("removeUnsavedEvents");
                  $calendar.weekCalendar("updateEvent", calEvent);
                  $dialogContent.dialog("close");
               },
               cancel : function() {
                  $dialogContent.dialog("close");
               }
            }
         }).show();

         $dialogContent.find(".date_holder").text($calendar.weekCalendar("formatDate", calEvent.start));
         setupStartAndEndTimeFields(startField, endField, calEvent, $calendar.weekCalendar("getTimeslotTimes", calEvent.start));

      },
				data: function(start, end, callback) {
					var dataSource = $('#data_source').val();
					if(dataSource === "1") {
						callback(eventData1);
					} else if(dataSource === "2") {
						callback(eventData2);
					} else {
						callback({options: {defaultFreeBusy:{free:true}}, events: []});
					}
	            },
				users: ['Mimí', 'Pepe colores', 'El Richard', 'Luciana tijereta'],
				showAsSeparateUser: true,
				displayOddEven: true,
				displayFreeBusys: true,
				daysToShow: 1,
				switchDisplay: {'1 day': 1, '3 next days': 3},
        headerSeparator: ' ',
        useShortDayNames: true,
        // I18N
        firstDayOfWeek: $.datepicker.regional['es'].firstDay,
        shortDays: $.datepicker.regional['es'].dayNamesShort,
        longDays: $.datepicker.regional['es'].dayNames,
        shortMonths: $.datepicker.regional['es'].monthNamesShort,
        longMonths: $.datepicker.regional['es'].monthNames,
        dateFormat: "d F y"
			});
	
			$('#data_source').change(function() {
				$calendar.weekCalendar("refresh");
				updateMessage();
			});
			
			updateMessage();
		});
  })(jQuery);
  function resetForm($dialogContent) {
      $dialogContent.find("input").val("");
      $dialogContent.find("textarea").val("");
   }
