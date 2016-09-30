using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyEvents
{
    public class RatingBehavior : Behavior<View>
    {
        TapGestureRecognizer tapRecognizer;
        static List<RatingBehavior> defaultBehaviors = new List<RatingBehavior>();
        static Dictionary<string, List<RatingBehavior>> starGroups = new Dictionary<string, List<RatingBehavior>>();

        public static readonly BindableProperty GroupNameProperty =
            BindableProperty.Create("GroupName",
                                    typeof(string),
                                    typeof(RatingBehavior),
                                    null,
                                    propertyChanged: OnGroupNameChanged);

        public string GroupName
        {
            set { SetValue(GroupNameProperty, value); }
            get { return (string)GetValue(GroupNameProperty); }
        }

        public static readonly BindableProperty RatingProperty =
           BindableProperty.Create("Rating",
                                   typeof(int),
                                   typeof(RatingBehavior), default(int));

        public int Rating
        {
            set { SetValue(RatingProperty, value); }
            get { return (int)GetValue(RatingProperty); }
        }

        static void OnGroupNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            RatingBehavior behavior = (RatingBehavior)bindable;
            string oldGroupName = (string)oldValue;
            string newGroupName = (string)newValue;

            // Remove existing behavior from Group
            if (String.IsNullOrEmpty(oldGroupName))
            {
                defaultBehaviors.Remove(behavior);
            }
            else
            {
                List<RatingBehavior> behaviors = starGroups[oldGroupName];
                behaviors.Remove(behavior);

                if (behaviors.Count == 0)
                {
                    starGroups.Remove(oldGroupName);
                }
            }

            // Add New Behavior to the group
            if (String.IsNullOrEmpty(newGroupName))
            {
                defaultBehaviors.Add(behavior);
            }
            else
            {
                List<RatingBehavior> behaviors = null;

                if (starGroups.ContainsKey(newGroupName))
                {
                    behaviors = starGroups[newGroupName];
                }
                else
                {
                    behaviors = new List<RatingBehavior>();
                    starGroups.Add(newGroupName, behaviors);
                }

                behaviors.Add(behavior);
            }

        }


        public static readonly BindableProperty IsStarredProperty =
            BindableProperty.Create("IsStarred",
                                    typeof(bool),
                                    typeof(RatingBehavior),
                                    false,
                                    propertyChanged: OnIsStarredChanged);

        public bool IsStarred
        {
            set { SetValue(IsStarredProperty, value); }
            get { return (bool)GetValue(IsStarredProperty); }
        }

        static void OnIsStarredChanged(BindableObject bindable, object oldValue, object newValue)
        {
            RatingBehavior behavior = (RatingBehavior)bindable;

            if ((bool)newValue)
            {
                string groupName = behavior.GroupName;
                List<RatingBehavior> behaviors = null;

                if (string.IsNullOrEmpty(groupName))
                {
                    behaviors = defaultBehaviors;
                }
                else
                {
                    behaviors = starGroups[groupName];
                }

                bool itemReached = false;
                int count = 1, position = 0;
                // all positions to left IsStarred = true and all position to the right is false
                foreach (var item in behaviors)
                {
                    if (item != behavior && !itemReached)
                    {
                        item.IsStarred = true;
                    }
                    if (item == behavior)
                    {
                        itemReached = true;
                        item.IsStarred = true;
                        position = count;
                    }
                    if (item != behavior && itemReached)
                        item.IsStarred = false;

                    item.Rating = position;
                    count++;
                }

            }


        }

        public RatingBehavior()
        {
            defaultBehaviors.Add(this);
        }

        protected override void OnAttachedTo(View view)
        {
            tapRecognizer = new TapGestureRecognizer();
            tapRecognizer.Tapped += OnTapRecognizerTapped;
            view.GestureRecognizers.Add(tapRecognizer);
        }

        protected override void OnDetachingFrom(View view)
        {
            view.GestureRecognizers.Remove(tapRecognizer);
            tapRecognizer.Tapped -= OnTapRecognizerTapped;
        }

        void OnTapRecognizerTapped(object sender, EventArgs args)
        {
            //HACK: PropertyChange does not fire, if the value is not changed :-(
            IsStarred = false;
            IsStarred = true;
        }
    }
}
